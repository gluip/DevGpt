using DevGpt.Console.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using TaskStatus = DevGpt.Console.Tasks.TaskStatus;

namespace DevGpt.Taskbased.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var project = new Project
            {
                Objective = "Write a new c# console application that can add two numbers. Run and test it",
                TaskList = new[]
                {
                    new DevGptTask
                    {
                        task = "Create a new project",
                        command = "execute_shell",
                        arguments = new[] { "dotnet new console -o myApp" },
                        id = 0,
                        status = TaskStatus.pending
                    },
                    new DevGptTask
                    {
                        task = "Compile the project",
                        command = "execute_shell",
                        arguments = new[] { "dotnet build myApp" },
                        id = 1,
                        status = TaskStatus.pending
                    }
                }
            };

        }

    }
}