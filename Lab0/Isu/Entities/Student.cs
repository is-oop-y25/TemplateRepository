using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(string name, Group group)
    {
        Name = name;
        Group = group;
        Id = StudentId.GenerateNewId();
    }

    public int Id { get; }
    public string Name { get; }
    public Group Group { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        LeaveCurrentGroup();
        AddToNewGroup(newGroup);
    }

    private void LeaveCurrentGroup()
    {
        Group.RemoveStudent(this);
    }

    private void AddToNewGroup(Group newGroup)
    {
        newGroup.AddStudent(this);
        Group = newGroup;
    }
}