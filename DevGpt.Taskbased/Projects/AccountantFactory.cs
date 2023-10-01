﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console.Tasks;
using TaskStatus = DevGpt.Console.Tasks.TaskStatus;

namespace DevGpt.Taskbased.Projects
{
    internal class AccountantFactory
    {
        public static Project GetProject()
        {
            return new Project
            {
                Objective =
                    "Read the file invoices.csv in the Invoices folder. For each PDF in the Invoices folder add a line to the csv containing the data of the invoice.",
                //    Environment.NewLine+ 
                //"Inspect the functionality in https://www.berekenhet.nl/kalender/weekdag-datum.html to determine what to test" + 
                //    Environment.NewLine +
                //"Create the project in a folder called 'weekdays' testing the webpage using xunit and playwright" +
                //    Environment.NewLine +
                //"Make sure the project compiles and all tests pass",

                TaskList = new[]
                {
                    new DevGptTask
                    {
                        task = "Create a new project",
                        command = "execute_shell",
                        arguments = new[] { "dotnet new console -o myApp" },
                        //dependent_task_ids = new List<int>(),
                        reason = "to create a cponsole app we need a project",
                        id = 0,
                        status = TaskStatus.pending,
                    },
                    new DevGptTask
                    {
                        task = "Compile the project",
                        command = "execute_shell",
                        arguments = new[] { "dotnet build myApp" },
                        //dependent_task_ids = new List<int> { 0 },
                        reason = "to verify the project compiles",
                        id = 1,
                        status = TaskStatus.pending
                    }
                }
            };
        }
    }
}