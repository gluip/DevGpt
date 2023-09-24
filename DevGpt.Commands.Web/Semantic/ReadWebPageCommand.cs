using System.Text;
using System.Text.Json;
using DevGpt.Commands.Web.Browser;
using DevGpt.Models;
using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Console.Commands.Semantic;

public class ReadWebPageCommand : IAsyncCommand
{
    private readonly IBrowser _browser;
    private readonly IMagicFunction _magicFunction;

    public ReadWebPageCommand(IBrowser browser,IMagicFunction magicFunction)
    {
        _browser = browser;
        _magicFunction = magicFunction;
    }

    public string Name => "read_web_page";
    public string Description => "Reads the given page and adds facts about the given topic to short term memory.";
    public string[] Arguments => new[] { "url" ,"topic"};

    public async Task<string> ExecuteAsync(string[] args)
    {
        if (args.Length != 2)
        {
            return $"{Name} requires 2 arguments: url and topic";
        }
        var url = args[0];
        var topic = args[1];

        await _browser.OpenPage(url);
        var pageText = await _browser.GetPageText();
        var facts = await _magicFunction.GetResults<IList<string>>($"Get the ten most important atomic simple facts about {topic} in the text in CONTEXT. Start each fact with {topic} is/was", pageText,new[]{"blue is a color","blue is a four letter word" });
        return JsonSerializer.Serialize(facts);
    }
}