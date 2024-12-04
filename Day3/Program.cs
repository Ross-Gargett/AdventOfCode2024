
using System.Text.RegularExpressions;

int Mul(int a, int b)
{
    return a * b;
}

List<(int a, int b)> ParseMult(string input)
{
    var matches = MultRegex().Matches(input);
    
    return matches.Select(m => 
        (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)))
        .ToList();
}

List<(int a, int b)> ParseDoMult(string input)
{
    var matches = DoDontMultRegex().Matches(input);
    var isEnabled = true;

    var result = new List<(int a, int b, bool isEnabled)>();

    // walk through the matches, setting isEnabled
    // based on the last seen do() or don't()
    foreach (Match match in matches)
    {
        // do()
        if (match.Groups[1].Success)
        {
            isEnabled = true;
        }
        // don't()
        else if (match.Groups[4].Success)
        {
            isEnabled = false;
        }
        else
        {
            // mul(a, b)
            var a = int.Parse(match.Groups[2].Value);
            var b = int.Parse(match.Groups[3].Value);
            result.Add((a, b, isEnabled));
        }
    }

    return result.Where(x => x.isEnabled)
        .Select(x => (x.a, x.b))
        .ToList();
}

int SolvePart1(string input)
{
    var multStatements = ParseMult(input);

    return multStatements.Sum(statement 
        => Mul(statement.a, statement.b));
}

int SolvePart2(string input)
{
    var doMultStatements = ParseDoMult(input);

    return doMultStatements.Sum(statement 
        => Mul(statement.a, statement.b));
}

var input = File.ReadAllText("input.txt");

Console.WriteLine($"Part 1: {SolvePart1(input)}");
Console.WriteLine($"Part 2: {SolvePart2(input)}");

partial class Program
{
    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MultRegex();
    
    [GeneratedRegex(@"(do\(\))|mul\((\d+),(\d+)\)|(don't\(\))")]
    private static partial Regex DoDontMultRegex();
}