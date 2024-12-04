bool IsReportSafe(List<int> report)
{
    var ascending = report[0] < report[1];

    for (var i = 0; i < report.Count - 1; i++)
    {
        var left = report[i];
        var right = report[i + 1];
        var diff = ascending 
            ? right - left 
            : left - right;

        if (diff is < 1 or > 3)
        {
            return false;
        }
    }

    return true;
}

bool IsReportSafeWithDampening(List<int> report)
{
    for (var i = 0; i < report.Count; i++)
    {
        var copy = new List<int>(report);
        copy.RemoveAt(i);

        //Console.WriteLine($"     Checking with {string.Join(", ", copy)}");

        if (!IsReportSafe(copy)) continue;
        
        //Console.WriteLine($"     Removing {report[i]} makes it safe");
        return true;
    }
    
    return false;
}

int SolvePart1(List<List<int>> reports)
{
    return reports.Count(IsReportSafe);
}

int SolvePart2(List<List<int>> reports)
{
    var safeCount = 0;
    
    foreach (var report in reports)
    {
        var isSafe = IsReportSafe(report);
        
        if (isSafe)
        {
            //Console.WriteLine($"Report: {string.Join(", ", report)} - Safe");
            safeCount++;
            continue;
        }
        
        // Console.WriteLine($"\n\n Trying with dampening: {string.Join(", ", report)}");
        if (IsReportSafeWithDampening(report))
        {
            safeCount++;
        }
    }

    return safeCount;
}

var input = File.ReadAllLines("input.txt");

var reports = new List<List<int>>();
foreach (var line in input)
{
    var split = line.Split(" ");
    var report = split.Select(int.Parse).ToList();
    reports.Add(report);
}

Console.WriteLine($"Part 1: {SolvePart1(reports)}");
Console.WriteLine($"Part 2: {SolvePart2(reports)}");