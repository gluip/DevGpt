﻿using DevGpt.Models.Commands;

namespace DevGpt.Console.Commands;

public class SearchFilesCommand : ICommand
{
    public string Execute(params string[] args)
    {
        if (args.Length != 2)
        {
            return $"{Name} requires 2 arguments: 'path' and 'searchPattern'. For example '.' and '*.*'";
        }

        try
        {
            var path = args[0];
            //make it relative to the current directory
            if (path.StartsWith("/"))
            {
                path = "." + path;
            }

            var searchPattern = args[1];
            var files = Directory.GetFiles(path, searchPattern,SearchOption.AllDirectories).Select(f=>Path.GetFullPath(f));
            //remove node_modules files
            files = files.Where(f => !f.Contains("node_modules"));

            if (!files.Any())
            {
                return $"no files found in '{path}' with pattern '{searchPattern}'";
            }

            return $"the command {Name} of '{path}' pattern '{searchPattern}' returned '{string.Join(",", files)}'";
        }
        catch (Exception ex)
        {
            return $"{Name} failed with the following error: {ex.Message}";
        }
    }

    public string Name => "search_files";
    public string Description => "recursively searches for files";
    public string[] Arguments => new[] { "path", "search_pattern" };
}