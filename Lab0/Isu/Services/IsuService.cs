using Isu.Entities;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<CourseNumber> _courses;
        private readonly Dictionary<Guid, Student> _studentsById;

        private Guid _studentId;

        public IsuService()
        {
            _studentsById = new Dictionary<Guid, Student>();
            _courses = new List<CourseNumber>();
            for (int i = 0; i < 4; i++)
                _courses.Add(new CourseNumber(i + 1));
        }

        public Group AddGroup(GroupName name)
        {
            var newGroup = new Group(name);

            _courses[name.Course - 1].AddGroup(newGroup);

            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group == null || FindGroup(group.Name) == null)
            {
                throw new IsuException("Group does not exist");
            }

            _studentId = Guid.NewGuid();
            var newStudent = new Student(name, _studentId, group.Name);

            _courses[group.Name.Course - 1].AddStudent(group, newStudent);
            _studentsById.Add(_studentId, newStudent);

            return newStudent;
        }

        public Student GetStudent(Guid id)
        {
            if (_studentsById.TryGetValue(id, out Student? student))
                return student;

            throw new IsuException("Student with this id does not exist");
        }

        public Student? FindStudent(Guid id) =>
            _courses.Where(course => course.FindStudent(id) != null).Select(course => course.FindStudent(id)).FirstOrDefault() ?? throw new IsuException("Student is null");

        public List<Student> FindStudents(GroupName groupName) =>
            _courses.Any(course => course.FindStudents(groupName) != null) ?
                _courses.First(course => course.FindStudents() != null).FindStudents() : new List<Student>();

        public List<Student> FindStudents(CourseNumber courseNumber) =>
            _courses.Contains(courseNumber) ? _courses.First(course => courseNumber == course).FindStudents() : new List<Student>();

        public Group? FindGroup(GroupName groupName) => _courses[groupName.Course - 1].FindGroup(groupName);

        public List<Group> FindGroups(CourseNumber courseNumber) => (_courses.Contains(courseNumber) ? _courses.First(course => courseNumber == course).GetGroups() : new List<Group>()).ToList();

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group? foundGroup = FindGroup(newGroup.Name);
            if (foundGroup == null)
                throw new IsuException("This group does not exist");

            FindGroup(student.GroupName)?.RemoveStudent(student);
            student.ChangeGroup(newGroup.Name);
            foundGroup.AddStudent(student);
        }
    }
}