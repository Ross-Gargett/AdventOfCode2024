// See https://aka.ms/new-console-template for more information

int SolvePart1(List<int> leftCol, List<int> rightCol)
{
    leftCol.Sort();
    rightCol.Sort();

    var total = leftCol.Zip(rightCol, (left, right) 
        => Math.Abs(left - right)).Sum();

    return total;
}

int SolvePart2(List<int> leftCol, List<int> rightCol)
{
    var instanceCounts = new Dictionary<int, int>();
    
    foreach (var item in rightCol.Where(item => !instanceCounts.TryAdd(item, 1)))
    {
        instanceCounts[item]++;
    }

    return leftCol.Sum(item => instanceCounts.TryGetValue(item, out var count)
        ? item * count
        : 0);
}

var input = File.ReadAllLines("input.txt");
var list1 = new List<int>();
var list2 = new List<int>();
foreach (var line in input)
{
    var split = line.Split("   ");
    list1.Add(int.Parse(split[0]));
    list2.Add(int.Parse(split[1]));
}
    
Console.WriteLine(SolvePart1(list1, list2));
Console.WriteLine(SolvePart2(list1, list2));