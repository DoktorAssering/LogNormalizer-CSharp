using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        string inputFile = "input.txt";
        string outputFile = "output.txt";
        string problemsFile = "problems.txt";

        using var reader = new StreamReader(inputFile, Encoding.GetEncoding("windows-1251"));
        using var writer = new StreamWriter(outputFile, false, Encoding.UTF8);
        using var problemWriter = new StreamWriter(problemsFile, false, Encoding.UTF8);

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();

            if (TryParseFormat1(line, out var parsed) || TryParseFormat2(line, out parsed))
            {
                writer.WriteLine(parsed.ToOutput());
            }
            else
            {
                problemWriter.WriteLine(line);
            }
        }

        Console.WriteLine("Обработка завершена.");
    }

    // ----------- FORMAT 1 -----------
    static bool TryParseFormat1(string line, out LogEntry entry)
    {
        entry = null;

        var match = Regex.Match(line,
            @"^(?<date>\d{2}\.\d{2}\.\d{4}) (?<time>\d{2}:\d{2}:\d{2}\.\d{3}) (?<level>[A-Z]+) (?<message>.+)$");

        if (!match.Success) return false;

        entry = new LogEntry
        {
            Date = match.Groups["date"].Value,
            Time = match.Groups["time"].Value,
            LevelRaw = match.Groups["level"].Value,
            LoggingLevel = MapLevel(match.Groups["level"].Value),
            Method = "DEFAULT",
            Message = match.Groups["message"].Value
        };
        return true;
    }

    // ----------- FORMAT 2 -----------
    static bool TryParseFormat2(string line, out LogEntry entry)
    {
        entry = null;

        var match = Regex.Match(line,
            @"^(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}\.\d{4})\s*\|\s*(?<level>\w+)\s*\|\s*(?<method>[^|]+)\s*\|\s*(?<message>.+)$");

        if (!match.Success) return false;

        DateTime.TryParse(match.Groups["date"].Value, out var dt);
        string formattedDate = dt.ToString("dd-MM-yyyy");

        entry = new LogEntry
        {
            Date = formattedDate,
            Time = match.Groups["time"].Value,
            LevelRaw = match.Groups["level"].Value,
            LoggingLevel = MapLevel(match.Groups["level"].Value),
            Method = match.Groups["method"].Value,
            Message = match.Groups["message"].Value
        };
        return true;
    }

    // ----------- LEVEL MAPPING -----------

    static string MapLevel(string level)
    {
        return level.ToUpper() switch
        {
            "INFO" or "INFORMATION" => "INFO",
            "WARN" or "WARNING" => "WARN",
            "DEBUG" => "DEBUG",
            "ERROR" => "ERROR",
            _ => "UNKNOWN"
        };
    }
}

// ----------- LOG ENTRY MODEL -----------

class LogEntry
{
    public string Date;
    public string Time;
    public string LoggingLevel;
    public string LevelRaw;
    public string Method;
    public string Message;

    public string ToOutput()
    {
        return $"{Date}\t{Time}\t{LoggingLevel}\t{LevelRaw}\t{Method}\t{Message}";
    }
}
