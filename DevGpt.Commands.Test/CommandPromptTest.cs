namespace DevGpt.Commands.Test
{
    public class CommandPromptTest
    {
        [Fact]
        public void CommandPrompt_Open_AndDir()
        {
            var commandPrompt = new CommandPrompt();
            commandPrompt.Open();
            commandPrompt.SendKeys("dir");
            commandPrompt.SendEnterKey();
            //Thread.Sleep(5000);
            var output = commandPrompt.ReadOutput();
            Assert.True(output.Contains("DevGpt"));

            commandPrompt.SendKeys("dir c:\\");
            output = commandPrompt.ReadOutput();


        }

        [Fact]
        public void CommandPrompt_Create_Vue_App()
        {
            var commandPrompt = new CommandPrompt();
            commandPrompt.Open();
            commandPrompt.SendLine("npm create vue@latest");
            //
                                 //"C:\\Program Files\\nodejs\\npm.cmd", "create vue@latest");
            //commandPrompt.SendKeys("npm create vue@latest");
            Thread.Sleep(2000);
            var output = commandPrompt.ReadOutput();
            Assert.True(output.Contains("Vue.js"));

            commandPrompt.SendKeys("\r\n");
            commandPrompt.SendKeys("\u001b[C");
            commandPrompt.SendKeys("\r\n");
            commandPrompt.SendKeys("\r\n");
            commandPrompt.SendKeys("\r\n");
            output = commandPrompt.ReadOutput();

            output = commandPrompt.ReadOutput();


        }
    }
}