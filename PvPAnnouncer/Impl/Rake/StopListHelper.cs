using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PvPAnnouncer.Impl.Rake;

// Attributed to https://github.com/benmcevoy/Rake under MIT - edits by spide-r

internal static class StopListHelper
{
    public static HashSet<string> ParseFromLanguage(string lang)
    {
        var stopWords = new HashSet<string>(StringComparer.Ordinal);

        var l = lang switch
        {
            "de" => "German",
            "fr" => "French",
            _ => "English"
        };
        var ll = ReadStopListLang(l);
        foreach (var line in ll)
        {
            var normalizedLine = line.AsSpan().Trim();

            if (normalizedLine.Length == 0 || normalizedLine[0] == '#') continue;

            var splitter = new StringSplitter(normalizedLine, ' ');

            while (splitter.TryGetNext(out var word)) stopWords.Add(word.ToString());
        }

        return stopWords;
    }


    private static IEnumerable<string> ReadStopListLang(string lang)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "PvPAnnouncer." + lang + "StopList.txt";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);

        while (reader.ReadLine() is { } line) yield return line;
    }
}