using Isu.Tools;

namespace Isu.Models;

public class GroupName
{
    private const int LENGTH = 5;
    private const int MAXNUMBER = 999;
    private const int MINNUMBER = 100;
    private const int MAXCOURSE = 6;

    private char[] _correctsSymbols = new char[] { 'A', 'B', 'C' };

    public GroupName(string name)
    {
        if (!IsValideGroupName(name))
        {
            throw new InvalideGroupNameException();
        }

        Name = name;
    }

    public string Name { get; private set; }

    public CourseNumber GetCourseNumber()
    {
        return (CourseNumber)int.Parse(Name[1].ToString());
    }

    private bool IsValideGroupName(string name)
    {
        return IsCorrectLength(name) && IsCorrectSymbol(name) && IsCorrectCourse(name) && IsCorrectNumber(name);
    }

    private bool IsCorrectLength(string name)
    {
        return name.Length == LENGTH;
    }

    private bool IsCorrectSymbol(string name)
    {
        char symbol = name[0];
        return _correctsSymbols.Contains(symbol);
    }

    private bool IsCorrectNumber(string name)
    {
        return int.TryParse(name[2..], out int number) && number >= MINNUMBER && number <= MAXNUMBER;
    }

    private bool IsCorrectCourse(string name)
    {
        return int.TryParse(name[1].ToString(), out int number) && number <= MAXCOURSE && number >= 1;
    }
}