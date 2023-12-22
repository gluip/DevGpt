// See https://aka.ms/new-console-template for more information

using System.Net;
using GenerativeAI.Models;
using GenerativeAI.Types;

Console.WriteLine("Hello, World!");

var apiKey = "AIzaSyCtDNNQUoK5faapQid6aLAEDtl9GDaKIxI";

var httpClient = new HttpClient(new HttpClientHandler()
{
    Proxy = new WebProxy("http://127.0.0.1:8888")
});
var model = new GenerativeModel(apiKey,client : httpClient);

var res = await model.GenerateContentAsync("How are you doing?");
