using DevGpt.Models.Commands;

namespace DevGpt.Console.Prompts;

internal abstract class PromptGeneratorBase : IPromptGenerator
{
    public string GetFullPrompt(IList<ICommandBase> commands)
    {
        var commandsText = commands.GetCommandsText();

        return GetUserPrompt(commandsText);
    }

    public abstract string GetUserPrompt(string commandsText);

    protected string GetGenericPromt()
    {
        return "Constraints:\r\n" +
            "1. ~4000 word limit for short term memory. Your short term memory is short, so immediately save important information to files.\r\n" +
            "2. If you are unsure how you previously did something or want to recall past events, thinking about similar events will help you remember.\r\n" +
            "3. No user assistance\r\n" +
            "4. Use tools when appropriate" +
            "" +
            "" +
            "" +
            "" +
            "Resources:\r\n" +
            "1. Internet access for searches and information gathering.\r\n" +
            "2. Long Term memory management.\r\n" +
            "3. File output.\r\n\r\n" +
            "" +
            "Performance Evaluation:\r\n" +
            "1. Continuously review and analyze your actions to ensure you are performing to the best of your abilities.\r\n" +
            "2. Constructively self-criticize your big-picture behavior constantly.\r\n" +
            "3. Reflect on past decisions and strategies to refine your approach.\r\n" +
            "4. Every command has a cost, so be smart and efficient. Aim to complete tasks in the least number of steps.\r\n\r\n" +
            "Use function calls on every response. respond in JSON format as described below\r\n\r\n" +
            "Response Format:\r\n" +
            "{" +
            "\r\n    \"thoughts\": " +
            "      {\r\n   \"text\": \"thought\",\r\n    " +
            "              \"reasoning\": \"reasoning\",\r\n    " +
            "              \"plan\": \"- short bulleted\\\\n- list that conveys\\\\n- long-term plan\",\r\n    " +
            "              \"criticism\": \"constructive self-criticism\",        \r\n    " +
            "\"speak\": \"thoughts summary to say to user\"\r\n     }\r\n" +
            "\r\n}\r\n         \r\n";
    }
}