using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TaskModel.Configuraion;
using TaskModel.Model;

namespace TaskModel.DataLoad
{
    public class DataLoadManager
    {
        private List<string> _mettings;
        private List<string> _doneStatuses;
        private TimeAnalyticConfigurationSection _config;
        public DataLoadManager()
        {
            _mettings = new List<string>();
            _doneStatuses = new List<string>();

            SpreadsheetInfo.SetLicense("E5M8-KYCM-HFC2-WRTR");
            FillSettings();
        }

        private void FillSettings()
        {
            _doneStatuses.Clear();
            _mettings.Clear();
            _config = ConfigurationManager.GetSection(TimeAnalyticConfigurationSection.SECTION_NAME) as TimeAnalyticConfigurationSection;
            if (_config == null)
                return;
            foreach (DoneStatusConfigurationElement configDoneStatus in _config.DoneStatuses )
            {
                _doneStatuses.Add(configDoneStatus.Status);
            }

            foreach (TaskConfigurationElement configTask in _config.Tasks)
            {
                if (configTask.IsMeeting)
                {
                    _mettings.Add(configTask.Key);
                }
            }
            
        }

        public ObservableCollection<TaskGroup> LoadTimeSheetReport(string fileName, DateTime dateFrom, DateTime dateTo)
        {
            if (!File.Exists(fileName))
                return null;
            List<WorkLogDataRow> fileData = ReadFile(fileName);
            if (fileData == null)
                return null;
            
            ObservableCollection<TaskGroup> groups = new ObservableCollection<TaskGroup>();
            TaskGroup summary = new TaskGroup { IsSummary = true, Title = "All" };
            groups.Add(summary);

            CalcGroups(fileData, groups, summary, dateFrom, dateTo);
            
            CalcTotals(groups);
            return groups;
        }

        private void CalcGroups(List<WorkLogDataRow> fileData, ObservableCollection<TaskGroup> groups, TaskGroup summary, DateTime dateFrom, DateTime dateTo)
        {
            foreach (WorkLogDataRow row in fileData)
            {
                if (row.BookDate >= dateFrom && row.BookDate <= dateTo)
                {
                    TaskGroup group = groups.FirstOrDefault(x => x.Id == row.Username);
                    if (group == null)
                    {
                        group = new TaskGroup();
                        group.Id = row.Username;
                        group.Title = row.UserFullName;
                        groups.Add(group);
                    }
                    
                    Task task = CreateTask(row);
                    AddTaskToGroup(group, task);
                    AddTaskToGroup(summary, task);
                }
            }
        }

        private void AddTaskToGroup(TaskGroup group, Task task)
        {
            Task taskCopy = task.ShallowCopy();
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

        private Task CreateTask(WorkLogDataRow row)
        {
            Task task = new Task();
            task.Key = row.IssueKey;
            task.KeyName = row.IssueKey;
            task.Url = _config.UrlPrefixToKey + task.Key;
            task.Status = row.IssueStatus;
            task.Title = row.IssueSummary;
            task.Estimation = GetDouble(row.IssueOriginalEstimate);
            task.LeftOnBegining = 0;
            task.TimeSpentByDev = GetDouble(row.Hours);
            task.IsDone = GetIsDone(row.IssueStatus);
            task.IsTaskRelatesToMettings = GetIsMeeting(row.IssueKey);
            task.IsTaskRelatesToDevelopment = !task.IsTaskRelatesToMettings;
            task.IsTaskManagedByDeveloper = true;
            return task;
        }

        private bool GetIsMeeting(string issueKey)
        {
            if (!string.IsNullOrEmpty(issueKey))
            {
                if (_mettings.Contains(issueKey))
                    return true;
            }
            return false;
        }

        private bool GetIsDone(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (_doneStatuses.Contains(status))
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

        private void CalcTotals(ObservableCollection<TaskGroup> groups)
        {
            foreach(var group in groups)
            {
                group.CalculateTotals();
            }
        }

        private List<WorkLogDataRow> ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            ExcelFile importFile = ExcelFile.Load(fileName);
            var ws = importFile.Worksheets["Worklogs"];
            if (ws == null)
                ws = importFile.Worksheets[0];

            List<WorkLogDataRow> rows = new List<WorkLogDataRow>();
            int rowIdx = 0;
            foreach (ExcelRow excelRow in ws.Rows)
            {
                if (rowIdx > 0)
                {
                    WorkLogDataRow row = ImportRow(excelRow);
                    if (row != null)
                        rows.Add(row);
                }
                rowIdx++;
            }
            return rows;
        }

        private WorkLogDataRow ImportRow(ExcelRow excelRow)
        {
            WorkLogDataRow row = new WorkLogDataRow();
            CellRange cells = excelRow.Cells;
            row.IssueKey = GetStringValue(cells, 0);
            row.IssueSummary = GetStringValue(cells, 1);
            row.Hours = GetStringValue(cells, 2);
            row.WorkDateStr = GetStringValue(cells, 3);
            row.Username = GetStringValue(cells, 4);
            row.UserFullName = GetStringValue(cells, 5);
            row.IssueStatus = GetStringValue(cells, 14);
            row.IssueOriginalEstimate = GetStringValue(cells, 22);
            row.IssueRemainingEstimate = GetStringValue(cells, 23);

            if (!string.IsNullOrEmpty(row.WorkDateStr))
            {
                DateTime dt;
                if (DateTime.TryParse(row.WorkDateStr, out dt))
                {
                    row.BookDate = dt;
                }
            }

            return row;
        }

        private string GetStringValue(CellRange excelRow, int position)
        {
            string value = null;
            ExcelCell cell = excelRow[position];
            if (cell.Value != null)
                value = cell.Value.ToString();
            return value;
        }

    }
}
