using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGpt.Console.Tasks;

namespace DevGpt.Taskbased.Projects
{
    internal class ConsoleAppFactory
    {
        public static Project GetConsoleAppProject()
        {
            return new Project
            {
                Objective = "Write a new c# console application that can add two numbers. Run and test it",
                TaskList = new[]
                {
                    new DevGptTask
                    {
                        task = "Create a new project",
                        command = "execute_shell",
                        arguments = new[] { "dotnet new console -o myApp" },
                        dependent_task_ids = new List<int>(),
                        id = 0,
                        status = "not_started",
                    },
                    new DevGptTask
                    {
                        task = "Compile the project",
                        command = "execute_shell",
                        arguments = new[] { "dotnet build myApp" },
                        dependent_task_ids = new List<int> { 0 },
                        id = 1,
                        status = "not_started"
                    }
                }
            };
        }
    }
}
