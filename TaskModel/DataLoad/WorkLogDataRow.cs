using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.DataLoad
{
    public class WorkLogDataRow
    {
        public string IssueKey;
        public string IssueSummary;
        public string Hours;
        public string WorkDateStr;
        public string Username;
        public string UserFullName;
        public string IssueStatus;
        public string IssueOriginalEstimate;
        public string IssueRemainingEstimate;

        public DateTime BookDate;
      

    }
}
