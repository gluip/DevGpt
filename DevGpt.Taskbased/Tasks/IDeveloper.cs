
namespace DevGpt.Console.Tasks;

interface IDeveloper
{
    Task<DevGptTask> ExecuteTask(string objective, DevGptTask task);
}