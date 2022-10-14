using Isu.Entities;
using Isu.Tools;

namespace Isu.Models
{
    public class CourseNumber
    {
        private readonly List<Group> _groups;

        public CourseNumber(int number)
        {
            _groups = new List<Group>();
            if (number <= 0)
                throw new IsuException($"Course number must be a positive number, received {number}");
            if (number >= 5)
                throw new IsuException($"Course number must be less than 5, received {number}");
            Number = number;
        }

        public int Number { get; }

        public void AddGroup(Group group)
        {
            if (group == null)
            {
                throw new IsuException("Group is null");
            }

            if (IsThisGroupExists(group.Name.Name))
            {
                throw new IsuException("This group already exists");
            }

            _groups.Add(group);
        }

        public void AddStudent(Group group, Student newStudent)
        {
            if (!IsThisGroupExists(group.Name.Name))
            {
                throw new IsuException("This group does not exist");
            }

            Group foundGroup = FindGroup(group.Name);
            if (foundGroup == null)
            {
                throw new IsuException("Group is null");
            }

            foundGroup.AddStudent(newStudent);
        }

        public bool IsThisGroupExists(string groupName) => _groups.Any(group => group.Name.Name == groupName);

        public IReadOnlyList<Group> GetGroups() => _groups;

        public Group FindGroup(GroupName groupName) => _groups.FirstOrDefault(group => Equals(group.Name, groupName))
                                                       ?? throw new IsuException("Group is null");

        public Student FindStudent(string name) =>
            _groups?.Where(group => group.FindStudent(name) != null)?.Select(group => group.FindStudent(name))
                 ?.FirstOrDefault() ?? throw new IsuException("Student is null");

        public Student FindStudent(Guid id) =>
            _groups?.Where(group => group.FindStudent(id) != null)?.Select(group => group.FindStudent(id))
                ?.FirstOrDefault() ?? throw new IsuException("Student is null");

        public List<Student> FindStudents(GroupName groupName) =>
            (_groups.Where(group => Equals(group.Name, groupName)).Select(group => group.GetStudents())
                .FirstOrDefault() ?? throw new IsuException("Group is null")).ToList();

        public List<Student> FindStudents() =>
            _groups.SelectMany(group => group.GetStudents()).ToList();
        }
}