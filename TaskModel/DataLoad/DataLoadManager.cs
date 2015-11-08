using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using TaskModel.Model;
using TaskModel.Settings;

namespace TaskModel.DataLoad
{
    public class DataLoadManager
    {
        public DataLoadManager()
        {
            SpreadsheetInfo.SetLicense("E5M8-KYCM-HFC2-WRTR");
        }
        

        public ObservableCollection<TaskGroup> LoadTimeSheetReport(string fileName, DateTime dateFrom, DateTime dateTo, ModelSettings settings)
        {
            if (!File.Exists(fileName))
                return null;
            List<WorkLogDataRow> fileData = ReadFile(fileName, settings);
            if (fileData == null)
                return null;
            
            ObservableCollection<TaskGroup> groups = new ObservableCollection<TaskGroup>();
            TaskGroup summary = new TaskGroup { IsSummary = true, Title = "All" };
            groups.Add(summary);

            CalcGroups(fileData, groups, summary, dateFrom, dateTo, settings);

            CalcTotals(groups, settings);
            return groups;
        }

        private void CalcGroups(List<WorkLogDataRow> fileData, ObservableCollection<TaskGroup> groups, TaskGroup summary, DateTime dateFrom, DateTime dateTo, ModelSettings settings)
        {
            foreach (WorkLogDataRow row in fileData)
            {
                if (row.Key == null)
                    continue;

                DateTime bookDate = GetDate(row.WorkDateStr, settings.SourceFileSettings.DateField);

                if (bookDate >= dateFrom && bookDate <= dateTo)
                {
                    TaskGroup group = groups.FirstOrDefault(x => x.Id == row.UserName);
                    if (group == null)
                    {
                        group = new TaskGroup();
                        group.Id = row.UserName;
                        group.Title = row.UserName;
                        groups.Add(group);
                    }

                    Task task = CreateTask(row, settings);
                    AddTaskToGroup(group, task);
                    AddTaskToGroup(summary, task);
                }
            }
        }

        private DateTime GetDate(string dateString, Settings.Fields.WorkDateField dateFieldDefinition)
        {
            string pattern = dateFieldDefinition.DateFormat;
            string toParse = dateString;
            if (toParse.Length > pattern.Length)
                toParse = toParse.Substring(0, pattern.Length);

            DateTime dt = DateTime.ParseExact(toParse, pattern, CultureInfo.InvariantCulture);
            return dt;
        }

        private void AddTaskToGroup(TaskGroup group, Task task)
        {
            Task taskCopy = task.ShallowCopy();
            if (group.IsSummary)
                taskCopy.IsTaskAssigned = true;
            Task groupExistingTask = group.Tasks.FirstOrDefault(x => x.Key == taskCopy.Key);
            if (groupExistingTask == null)
            {
                group.Tasks.Add(taskCopy);
            }
            else
            {
                AddDataToTask(groupExistingTask, taskCopy);
            }
        }

        private void AddDataToTask(Task existingTask, Task extraData)
        {
            existingTask.TimeSpentByDev += extraData.TimeSpentByDev;
        }

        private Task CreateTask(WorkLogDataRow row, ModelSettings settings)
        {
            Task task = new Task();
            task.Key = row.Key;
            task.KeyName = row.Key;
            if (string.IsNullOrEmpty(settings.SourceFileSettings.UrlField.PrefixToValue))
                task.Url = row.Url;
            else
                task.Url = settings.SourceFileSettings.UrlField.PrefixToValue + row.Url;

            task.Status = row.Status;
            task.Title = row.Title;
            double estimation = 0;
            if (settings.SourceFileSettings.EstimationField.IsUseStoryPointsFromMainPosition)
            {
                double estimationStoryPoints =  GetDouble(row.OriginalEstimateStoryPoints);
                double cost = settings.SourceFileSettings.EstimationField.StoryPointCost;
                estimation = estimationStoryPoints * cost;
                if (estimation  < 0.000001)
                {
                    estimation = GetDouble(row.OriginalEstimateHours);
                }
            }
            else
            {
                estimation =  GetDouble(row.OriginalEstimateHours);
            }
            task.Estimation = estimation;
            task.TimeSpentByDev = GetDouble(row.TimeSpent);

            task.IsDone = GetIsDone(row.Status, settings.DoneStatuses);
            task.IsTaskRelatesToMettings = GetIsMeeting(row.Key, settings.MeetingTasks);
            task.IsTaskRelatesToDevelopment = !task.IsTaskRelatesToMettings;
            task.IsTaskAssigned = GetIsTaskAssigned(row.UserName, row.Assignee, settings.SourceFileSettings.AssigneeField.NameMapping);
            return task;
        }

        private bool GetIsTaskAssigned(string userName, string assignee, ICollection<ValueMapping> userNameMappings)
        {
            if (string.Compare(userName, assignee, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return true;
            }
            var mapping = userNameMappings.FirstOrDefault(x => string.Compare(x.Source, assignee, StringComparison.CurrentCultureIgnoreCase) == 0);
            if (mapping == null)
                return false;
            if (string.Compare(userName, mapping.Target, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return true;
            }
            return false;
        }

        private bool GetIsMeeting(string issueKey, ICollection<SpecialTask> meetingTasks)
        {
            if (!string.IsNullOrEmpty(issueKey))
            {
                var meetingTask = meetingTasks.FirstOrDefault(x => x.Key == issueKey);
                if (meetingTask != null)
                    return true;
            }
            return false;
        }

        private bool GetIsDone(string status, ICollection<DoneStatus> statuses)
        {
            if (!string.IsNullOrEmpty(status))
            {
                var foundItem = statuses.FirstOrDefault(x => x.Name == status);
                if (foundItem != null)
                    return true;
            }
            return false;
        }

        private double GetDouble(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            string strValue = value.Replace(",", ".");
            return double.Parse(strValue, CultureInfo.InvariantCulture);
        }

        private void CalcTotals(ObservableCollection<TaskGroup> groups, ModelSettings settings)
        {
            foreach(var group in groups)
            {
                group.CalculateTotals(settings);
            }
        }

        private List<WorkLogDataRow> ReadFile(string fileName, ModelSettings settings)
        {
            if (!File.Exists(fileName))
                return null;

            ExcelFile importFile = ExcelFile.Load(fileName);
            var ws = importFile.Worksheets[0];
            if (!string.IsNullOrEmpty(settings.ExcelTabName) && importFile.Worksheets.Contains(settings.ExcelTabName))
            {
                ws = importFile.Worksheets[settings.ExcelTabName];
            }


            List<WorkLogDataRow> rows = new List<WorkLogDataRow>();
            int rowIdx = 0;
            foreach (ExcelRow excelRow in ws.Rows)
            {
                if (rowIdx > 0)
                {
                    WorkLogDataRow row = ImportRow(excelRow, settings.SourceFileSettings);
                    if (row != null)
                        rows.Add(row);
                }
                rowIdx++;
            }
            return rows;
        }

        private WorkLogDataRow ImportRow(ExcelRow excelRow, SourceFileSettings settings)
        {
            WorkLogDataRow row = new WorkLogDataRow();
            CellRange cells = excelRow.Cells;

            row.Key = GetStringValue(cells, settings.KeyField.Position - 1);
            row.Title = GetStringValue(cells, settings.TitleField.Position - 1);
            row.Assignee = GetStringValue(cells, settings.AssigneeField.Position - 1);
            row.Status = GetStringValue(cells, settings.StatusField.Position - 1);
            row.WorkDateStr = GetStringValue(cells, settings.DateField.Position - 1);
            row.UserName = GetStringValue(cells, settings.UserNameField.Position - 1);
            row.Url = GetStringValue(cells, settings.UrlField.Position - 1);

            if (settings.EstimationField.IsUseStoryPointsFromMainPosition)
            {
                row.OriginalEstimateHours = GetStringValue(cells, settings.EstimationField.AlternatePosition - 1);
                row.OriginalEstimateStoryPoints = GetStringValue(cells, settings.EstimationField.Position - 1);
            }
            else
            {
                row.OriginalEstimateHours = GetStringValue(cells, settings.EstimationField.Position - 1);
                row.OriginalEstimateStoryPoints = "0";
            }
            row.OriginalEstimateHours = RemoveSymbol(row.OriginalEstimateHours, 'h');
            row.OriginalEstimateStoryPoints = RemoveSymbol(row.OriginalEstimateStoryPoints, 'h');

            row.TimeSpent = RemoveSymbol(GetStringValue(cells, settings.TimeSpentField.Position - 1), 'h');

            return row;
        }

        private string RemoveSymbol(string source, char toTrim)
        {
            if (string.IsNullOrEmpty(source))
                return source;
            char toTrimUpper = toTrim.ToString().ToUpperInvariant()[0];

            string result = source.TrimEnd(toTrim).TrimEnd(toTrimUpper);
            return result;
        }

        private string GetStringValue(CellRange excelRow, int position)
        {
            string value = null;
            ExcelCell cell = excelRow[position];
            if (cell.Value != null)
            {
                value = cell.Value.ToString();
                value = value.Trim();
            }

            return value;
        }

    }
}
