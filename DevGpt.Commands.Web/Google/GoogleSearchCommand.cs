using System.Text.Json;
using DevGpt.Models.Commands;

namespace DevGpt.Commands.Web.Google;

public class GoogleSearchCommand : IAsyncCommand
{
    private readonly HttpClient client;

    public GoogleSearchCommand()
    {
         client = new HttpClient();
    }

    public string Name => "perform_google_search";
    public string Description => "Performs a google search and returns the top hits in json format";
    public string[] Arguments => new[] { "term" };

    public async Task<string> ExecuteAsync(string[] args)
    {
        if (args.Length != 1)
        {
            return $"{Name} requires 1 argument: searchterm";
        }

        try
        {
            var googleApiKey = "AIzaSyDWU-xr6VsqvgnYU441_F2C6HQwxtDS9Ww";
            var googleSearchEngineId = "b6176df06771e42a1";

            // make a rest request to google search api
            var url = $"https://www.googleapis.com/customsearch/v1?key={googleApiKey}&cx={googleSearchEngineId}&q={args[0]}";
            var resultContent = await client.GetStringAsync(url);
            var googleSearchResult = JsonSerializer.Deserialize<GoogleJsonRoot>(resultContent);
            var result = JsonSerializer.Serialize(googleSearchResult);
            return result;

            //return $"{Name} of {url} returned : " + JsonSerializer.Serialize(result);
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }
}

public class GoogleSearchResult
{
    public string Title { get; set; }
    public string Url { get; set; }
}

