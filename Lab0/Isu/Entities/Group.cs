using Isu.Models;
using Isu.Tools;
namespace Isu.Entities;

public class Group
{
    private const int MAXSTUDENTCOUNT = 30;

    public Group(GroupName groupName)
    {
        StudentsList = new List<Student>();

        GroupName = groupName;
        CourseNumber = groupName.GetCourseNumber();
    }

    public List<Student> StudentsList { get; }
    public CourseNumber CourseNumber { get;  }
    public GroupName GroupName { get; }

    public void AddStudent(Student student)
    {
        if (GetCountVacandePlaces() == 0)
            throw new AddToFullGroupException();
        StudentsList.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        StudentsList.Remove(student);
    }

    public int GetCountVacandePlaces()
    {
        return MAXSTUDENTCOUNT - StudentsList.Count;
    }
}
