using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TaskModel.Model;

namespace TaskModel.DataLoad
{
    public class DataExport
    {
        public void Export(string fileName, DateTime dateFrom, DateTime dateTo, IEnumerable<TaskGroup> groups)
        {
            SpreadsheetInfo.SetLicense("E5M8-KYCM-HFC2-WRTR");
            if (File.Exists(fileName))
                File.Delete(fileName);

            if (groups == null)
                throw new ApplicationException("no data");

            var workbook = new ExcelFile();
            // Create new worksheet and set cell A1 value to 'Hello world!'.
            ExcelWorksheet ws = workbook.Worksheets.Add("TimeAnalytic");
            int row = 0; //numeration starts from 0
            WriteDate(ws, 1, "Date From", dateFrom);
            WriteDate(ws, 2, "Date To", dateTo);
            row = 4;
            WriteGroupHeader(ws, row);
            row++;
            TaskGroup summary = groups.FirstOrDefault(x => x.IsSummary);
            WriteGroupHeader(ws, row);
            row = WriteGroups(ws, groups, summary, ref row);
            row++;
            row++;
            WriteTasksHeader(ws, row);
            WriteTasksForGroups(ws, groups, summary, ref row);
            workbook.Save(fileName);
        }

        private void WriteTasksHeader(ExcelWorksheet ws, int row)
        {
            ws.Cells[row, (int)TaskDataPosition.Key].Value = "Key";
            ws.Cells[row, (int)TaskDataPosition.Title].Value = "Title";
            ws.Cells[row, (int)TaskDataPosition.Estimation].Value = "Estimation";
            ws.Cells[row, (int)TaskDataPosition.TimeSpentByDev].Value = "Time Spent";
            ws.Cells[row, (int)TaskDataPosition.IsDone].Value = "Is Done";
            ws.Cells[row, (int)TaskDataPosition.IsTaskRelatesToMettings].Value = "Is Metting";
            ws.Cells[row, (int)TaskDataPosition.IsTaskRelatesToDevelopment].Value = "Is Development";
            ws.Cells[row, (int)TaskDataPosition.IsTaskAssigned].Value = "Is Assigned Task";
            ws.Cells[row, (int)TaskDataPosition.Status].Value = "Status";
            ws.Cells[row, (int)TaskDataPosition.UnderEstimate].Value = "Under Estimate";
            ws.Cells[row, (int)TaskDataPosition.Url].Value = "Url";
        }

        private void WriteTaskData(ExcelWorksheet ws, Task task, int row)
        {
            ws.Cells[row, (int)TaskDataPosition.Key].Value = task.KeyName;
            ws.Cells[row, (int)TaskDataPosition.Title].Value = task.Title;
            ws.Cells[row, (int)TaskDataPosition.Estimation].Value = DoubleToString(task.Estimation);
            ws.Cells[row, (int)TaskDataPosition.TimeSpentByDev].Value = DoubleToString(task.TimeSpentByDev);
            ws.Cells[row, (int)TaskDataPosition.IsDone].Value = BoolToString(task.IsDone);
            ws.Cells[row, (int)TaskDataPosition.IsTaskRelatesToMettings].Value = BoolToString(task.IsTaskRelatesToMettings);
            ws.Cells[row, (int)TaskDataPosition.IsTaskRelatesToDevelopment].Value = BoolToString(task.IsTaskRelatesToDevelopment);
            ws.Cells[row, (int)TaskDataPosition.IsTaskAssigned].Value = BoolToString(task.IsTaskAssigned);
            ws.Cells[row, (int)TaskDataPosition.Status].Value = task.Status;
            ws.Cells[row, (int)TaskDataPosition.UnderEstimate].Value = DoubleToString(task.UnderEstimate);
            ws.Cells[row, (int)TaskDataPosition.Url].Value = task.Url;
        }

        private object BoolToString(bool value)
        {
            return value ? "True" : "False";
        }

        private string DoubleToString(double value)
        {
            double x = Math.Truncate(value * 100) / 100;
            string s = string.Format("{0:N2}", x);
            return s;
        }
        private string DateToString(DateTime date)
        {

            string dateStr = date.ToString("yyyy-MM-dd");
            return dateStr;
        }

        private void WriteGroupHeader(ExcelWorksheet ws, int row)
        {
            ws.Cells[row, (int)TaskGroupDataPosition.Title].Value = "Key";
            ws.Cells[row, (int)TaskGroupDataPosition.TotalEstimationDevelopment].Value = "Total Estimation";
            ws.Cells[row, (int)TaskGroupDataPosition.TotalDoneEstimationDevelopment].Value = "Total Done (Estimation)";
            ws.Cells[row, (int)TaskGroupDataPosition.TotalDoneBookedDevelopment].Value = "Total Done (Booked)";
            ws.Cells[row, (int)TaskGroupDataPosition.TotalTimeBooked].Value = "Total Booked";
            ws.Cells[row, (int)TaskGroupDataPosition.TotalBookedDevelopment].Value = "Total Development";
            ws.Cells[row, (int)TaskGroupDataPosition.TotalBookedMeetings].Value = "Total Meetings";
            ws.Cells[row, (int)TaskGroupDataPosition.TotalUnderEstimate].Value = "Under Estimate";
            ws.Cells[row, (int)TaskGroupDataPosition.RateDoneBookedToBookedDevelopment].Value = "Done(Booked)/Development";
        }

        private void WriteGroupData(ExcelWorksheet ws, TaskGroup group, int row)
        {
            ws.Cells[row, (int)TaskGroupDataPosition.Title].Value = group.Title;
            ws.Cells[row, (int)TaskGroupDataPosition.TotalEstimationDevelopment].Value = DoubleToString(group.TotalEstimationDevelopment);
            ws.Cells[row, (int)TaskGroupDataPosition.TotalDoneEstimationDevelopment].Value = DoubleToString(group.TotalDoneEstimationDevelopment);
            ws.Cells[row, (int)TaskGroupDataPosition.TotalDoneBookedDevelopment].Value = DoubleToString(group.TotalDoneBookedDevelopment);
            ws.Cells[row, (int)TaskGroupDataPosition.TotalTimeBooked].Value = DoubleToString(group.TotalTimeBooked);
            ws.Cells[row, (int)TaskGroupDataPosition.TotalBookedDevelopment].Value = DoubleToString(group.TotalBookedDevelopment);
            ws.Cells[row, (int)TaskGroupDataPosition.TotalBookedMeetings].Value = DoubleToString(group.TotalBookedMeetings);
            ws.Cells[row, (int)TaskGroupDataPosition.TotalUnderEstimate].Value = DoubleToString(group.TotalUnderEstimate);
            ws.Cells[row, (int)TaskGroupDataPosition.RateDoneBookedToBookedDevelopment].Value = DoubleToString(group.RateDoneBookedToBookedDevelopment);
        }


        private int WriteGroups(ExcelWorksheet ws, IEnumerable<TaskGroup> groups, TaskGroup summary, ref int row)
        {
            if (summary != null)
            {
                WriteGroupData(ws, summary, row);
                row++;
            }

            foreach (TaskGroup group in groups)
            {
                if (group != summary)
                {
                    WriteGroupData(ws, group, row);
                    row++;
                }
            }
            return row;
        }
        private void WriteTasksForGroup(ExcelWorksheet ws, TaskGroup group, ref int row)
        {
            row++;
            ws.Cells[row, 1].Value = group.Title;
            row++;
            foreach (Task task in group.Tasks)
            {
                WriteTaskData(ws, task, row);
                row++;
            }
        }



        private void WriteTasksForGroups(ExcelWorksheet ws, IEnumerable<TaskGroup> groups, TaskGroup summary, ref int row)
        {
            if (summary != null)
            {
                WriteTasksForGroup(ws, summary, ref row);
                row++;
            }

            foreach (TaskGroup group in groups)
            {
                if (group != summary)
                {
                    WriteTasksForGroup(ws, group, ref row);
                    row++;
                }
            }

        }


        private void WriteDate(ExcelWorksheet ws, int row, string caption, DateTime date)
        {
            ws.Cells[row, 1].Value = caption;
            ws.Cells[row, 2].Value = DateToString(date);
        }
    }

    internal enum TaskDataPosition
    {
        Key = 1,
        Title,
        Estimation,
        TimeSpentByDev,
        IsDone,
        IsTaskRelatesToMettings,
        IsTaskRelatesToDevelopment,
        IsTaskAssigned,
        Status,
        UnderEstimate,
        Url
    }

    internal enum TaskGroupDataPosition
    {
        Title = 1,
        TotalEstimationDevelopment,
        TotalDoneEstimationDevelopment,
        TotalDoneBookedDevelopment,
        TotalTimeBooked,
        TotalBookedDevelopment,
        TotalBookedMeetings,
        TotalUnderEstimate,
        RateDoneBookedToBookedDevelopment
    }
}
