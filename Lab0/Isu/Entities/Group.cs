using Isu.Models;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private readonly int _maxStudents;
        private readonly List<Student> _students;

        public Group(GroupName name)
            : this(name, 10)
        {
        }

        public Group(GroupName name, int maxStudents)
        {
            _students = new List<Student>();
            Name = name ?? throw new IsuException("GroupName is null");
            _maxStudents = maxStudents;
        }

        public GroupName Name { get; }
        private int StudentsNow => _students.Count;

        public void AddStudent(Student newStudent)
        {
            if (StudentsNow == _maxStudents)
                throw new IsuException("The number of students exceeds the maximum");

            if (newStudent == null)
                throw new IsuException("Student is null");

            if (IsThisStudentExists(newStudent))
                throw new IsuException("This student is already in this group");

            _students?.Add(newStudent);
        }

        public bool IsThisStudentExists(Student student) => _students.Contains(student);
        public bool IsThisStudentExists(string name) => _students.Exists(student => student.Name == name);

        public Student FindStudent(string name) => _students.FirstOrDefault(student => student.Name == name)
                                                   ?? throw new IsuException("Student is null");

        public Student FindStudent(Guid id) => _students.FirstOrDefault(student => student.Id == id)
                                                   ?? throw new IsuException("Student is null");

        public IReadOnlyList<Student> GetStudents() => _students;

        public void RemoveStudent(Student student)
        {
            if (!IsThisStudentExists(student))
                throw new IsuException("Student does not Exist");

            _students.Remove(student);
        }
    }
}