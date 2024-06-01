using System.Text.Json;
using DevGpt.Commands.Commands;
using DevGpt.Commands.Windows;
using DevGpt.Commands.Windows.Drawing;
using DevGpt.Models.Commands;
using DevGpt.Models.OpenAI;
using DevGpt.OpenAI;
using MyApp;

namespace DevGpt.Forms
{
    public partial class Form1 : Form
    {
        private readonly DotnetOpenAIClient client;
        private GetScreenShotCommand _getScreenShotCommand = new GetScreenShotCommand();

        public Form1()
        {
            InitializeComponent();

            client = new DotnetOpenAIClient(OpenAiClientType.OpenAI);

        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
             var imageContext = new ImageContext();
            var commands = new List<ICommandBase>
            {
                new LoadImageCommand(imageContext),
                new DrawRectangleCommand(imageContext),
                new LookAtImageCommand(imageContext),
                new SaveImageCommnad(imageContext),
                //new GetCurrentDateCommand(),
                //new ClickMouseCommand(),
                //new MoveMouseCommand(),
                //new MoveMouseByDirectionCommand(),
                //getScreenShotCommand
                //new ReadFileCommand(),
                //new WriteFileCommand(),
                //new SearchFilesCommand(),
                //new ShutDownCommand(),
                //new ExecuteShellCommand(),
                //new DotnetAddReferenceCommand(),
                //new ReadPdfCommand(),
                ////new GoogleSearchCommand(),
                //new BrowserOpenCommand(browser),
                //new BrowserGetHtmlCommand(browser),
                //new BrowserEnterInputCommand(browser),
                //new BrowserClickCommand(browser),
                //new BrowserTakeScreenshotCommand(browser),
                //new ImageQuestionCommand(client),
                //new ReadWebPageCommand(browser,simpleFunction),
                //new ReadWebPageHtmlCommand(browser,simpleFunction)
            };

            var chatHandler = new ChatHandler();
            var commandExecutor = new CommandExecutor(commands);


            var generator = new PrompGenerator();
            var newPrompt = "Load the image from C:\\DevGpt\\catsdogs.jpg. Look at the image and draw a rectangle around the cats in the image.";
            txtPrompt.Text = newPrompt;
            var prompt = generator.GetFullPrompt(txtPrompt.Text);
            chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.System, prompt));

            while (true)
            {
                var devGptChatMessages = chatHandler.GetMessages();
                var devGptChatResponse = await client.CompletePrompt(devGptChatMessages, commands);
                chatHandler.RemoveContextMessages();

                chatHandler.AddMessage(devGptChatResponse);

                try
                {
                    WriteReply(devGptChatResponse);
                    if (devGptChatResponse.ToolCalls.Any())
                    {
                        foreach (var toolCall in devGptChatResponse.ToolCalls)
                        {

                            //check if command exists
                            if (commands.All(c => c.Name != toolCall.ToolName))
                            {
                                chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User,
                                    $"Tool '{toolCall.ToolName}' not found. Proceed to formulate a concrete command."));
                                continue;
                            }

                            //prompt user in a y/n question
                            txtLog.AppendText(JsonSerializer.Serialize(toolCall) + Environment.NewLine);
                            var resultMessages = await commandExecutor.ExecuteTool(toolCall);

                            foreach (var message in resultMessages)
                            {
                                chatHandler.AddMessage(message);
                            }
                        }
                    }
                    else
                    {
                        chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User,
                            $"No tool call found. Please make sure to include a tool call in every response"));
                    }
                }
                catch (Exception ex)
                {
                    chatHandler.AddMessage(new DevGptChatMessage(DevGptChatRole.User,
                        $"Unable to parse the response as json. Please provide a proper formatted respose.Error : " +
                        ex.Message));
                }
            }
        }

        private void WriteReply(DevGptChatMessage devGptChatResponse)
        {

            //var contentMessage = devGptChatResponse.Content.FirstOrDefault()?.Content;
            txtLog.AppendText($"{devGptChatResponse.Role}: {devGptChatResponse.Content.FirstOrDefault().Content}{Environment.NewLine}");
        }

        private async void btnScreensho_Click(object sender, EventArgs e)
        {
            await _getScreenShotCommand.ExecuteAsync(Enumerable.Empty<string>().ToArray());
        }
    }
}

