using Isu.Models;
using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        public Student(string name, Guid id, GroupName groupName)
        {
            Name = name ?? throw new IsuException("Student name is null");
            Id = id;
            GroupName = groupName ?? throw new IsuException("Group name is null");
        }

        public GroupName GroupName { get; private set; }

        public Guid Id { get; }

        public string Name { get; }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || GetType() != obj.GetType())
                return false;

            var student = (Student)obj;
            return (Name == student.Name) && (Id == student.Id) && GroupName.Equals(student.GroupName);
        }

        public override int GetHashCode() =>
            HashCode.Combine(Id, Name);

        public void ChangeGroup(GroupName newGroup) => GroupName = newGroup;
    }
}