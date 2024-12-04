
using System.Text.RegularExpressions;

int Mul(int a, int b)
{
    return a * b;
}

List<(int a, int b)> ParseMult(string input)
{
    var matches = Regex.Matches(input, @"mul\((\d+),(\d+)\)");
    
    return matches.Select(m => (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value))).ToList();
}

int SolvePart1(string input)
{
    var multStatements = ParseMult(input);

    return multStatements.Sum(statement 
        => Mul(statement.a, statement.b));
}

int SolvePart2(string input)
{
    return 0;
}

var input = File.ReadAllText("input.txt");

Console.WriteLine($"Part 1: {SolvePart1(input)}");
Console.WriteLine($"Part 2: {SolvePart2(input)}");