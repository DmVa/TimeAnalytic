﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TaskModel.Model;
using TaskModel.Settings;

namespace TaskModel.DataLoad
{
    class TestHelper
    {
        public static ObservableCollection<TaskGroup> CreateTestData()
        {
            ModelSettings settings = new ModelSettings();
            TaskGroup group1 =  new TaskGroup();
            group1.Title = "Test 1";
            group1.Tasks.Add(CreateTestTask());
            group1.Tasks.Add(CreateTestTask());
            group1.CalculateTotals(settings);

            TaskGroup group2 =  new TaskGroup();
            group2.Title = "Test 2";
            group2.Tasks.Add(CreateTestTask());
            group2.Tasks.Add(CreateTestTask());
            group2.CalculateTotals(settings);
            ObservableCollection<TaskGroup> groups = new ObservableCollection<TaskGroup>();
            groups.Add(group1);
            groups.Add(group2);

            return groups;
        }

        private static Task CreateTestTask()
        {
            Task task = new Task();
            task.KeyName = "TEST";
            task.Estimation = 10;
            task.IsTaskRelatesToDevelopment = true;
            task.Url = "https://preciseq.atlassian.net/browse/AL-768";

            return task;
        }
    }
}
