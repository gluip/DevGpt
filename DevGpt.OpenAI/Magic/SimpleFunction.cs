﻿using Azure.AI.OpenAI;
using DevGpt.Models.OpenAI;
using DevGpt.OpenAI;

namespace DevGpt.Commands.Magic;

public interface ISimpleFunction
{
    Task<string> GetResults(string question, string context);
}
public class SimpleFunction : ISimpleFunction
{
    private readonly IDevGptOpenAIClient _openAiClient;

    public SimpleFunction(IDevGptOpenAIClient openAiClient)
    {
        _openAiClient = openAiClient;
    }

    public async Task<string> GetResults(string question, string context)
    {
        var prompt =
            $"{question} context: {context}";

        return await _openAiClient.CompletePrompt(new List<DevGptChatMessage>{ new DevGptChatMessage(DevGptChatRole.User, prompt) });


    }
}