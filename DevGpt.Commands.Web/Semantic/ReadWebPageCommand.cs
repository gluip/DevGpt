using System.Text;
using System.Text.Json;
using DevGpt.Commands.Magic;
using DevGpt.Commands.Web.Browser;
using DevGpt.Models;
using DevGpt.Models.Commands;
using HtmlAgilityPack;

namespace DevGpt.Console.Commands.Semantic;

public class ReadWebPageCommand : IAsyncCommand
{
    private readonly IBrowser _browser;
    private readonly ISimpleFunction _magicFunction;

    public ReadWebPageCommand(IBrowser browser,ISimpleFunction magicFunction)
    {
        _browser = browser;
        _magicFunction = magicFunction;
    }

    public string Name => "read_web_page";
    public string Description => "Reads the given page and tries to answer te given question. For example get a summary of the text on the page.";
    public string[] Arguments => new[] { "url" ,"goal"};

    public async Task<string> ExecuteAsync(string[] args)
    {
        if (args.Length != 2)
        {
            return $"{Name} requires 2 arguments: url and goal";
        }
        var url = args[0];
        var question = args[1];

        await _browser.OpenPage(url);
        var pageText = await _browser.GetPageText();
        return await _magicFunction.GetResults(question,pageText);
    }
}