using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevGpt.Console.Tasks
{
    internal class TaskReasoningEngine
    {
        
        public TaskReasoningEngine()
        {
        }

        public void SolveObjective(string objective, IList<string> tasks)
        {
            //solve the first task on the list given the objective

        }
    }


    interface IProductOwner
    {
        System.Threading.Tasks.Task PrioritizeTasks(string objective, IList<string> tasks);
    }
    
}
