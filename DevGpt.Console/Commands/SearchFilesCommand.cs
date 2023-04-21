﻿namespace DevGpt.Console.Commands;

public class SearchFilesCommand : ICommand
{
    public string Execute(params string[] args)
    {
        if (args.Length != 2)
        {
            return $"{Name} requires 2 arguments path and searchPattern";
        }

        try
        {
            var path = args[0];
            var searchPattern = args[1];
            var files = Directory.GetFiles(path, searchPattern).Select(f=>Path.GetFullPath(f));
            return $"{Name} of '{path}' pattern '{searchPattern}' returned '{string.Join(",", files)}'";
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }

    public string Name => "search_files";
    public string Description => "searches for files";
    public string[] Arguments => new[] { "path", "search_pattern" };
}