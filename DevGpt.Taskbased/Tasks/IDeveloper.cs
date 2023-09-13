
namespace DevGpt.Console.Tasks;

interface IDeveloper
{
    System.Threading.Tasks.Task ExecuteTask(string objective, string task);
}