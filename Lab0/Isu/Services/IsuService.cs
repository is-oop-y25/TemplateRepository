using Isu.Entities;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<Group> _groups;
        public IsuService()
        {
            _groups = new List<Group>();
        }

        public Group AddGroup(GroupName name)
        {
            var group = new Group(name);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            var student = new Student(name, group);

            group.AddStudent(student);

            return student;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.ChangeGroup(newGroup);
        }

        public Group? FindGroup(GroupName groupName)
        {
            return _groups.Where(group => group.GroupName.Equals(groupName)).FirstOrDefault();
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(group => group.CourseNumber.Equals(courseNumber)).ToList();
        }

        public Student? FindStudent(int id)
        {
            Student? student = (from g in _groups
                               from s in g.StudentsList
                               where s.Id.Equals(id)
                               select s).FirstOrDefault();
            return student;
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            List<Student>? resList = (from g in _groups
                                      where g.GroupName.Equals(groupName)
                                      select g).FirstOrDefault()?.StudentsList;
            return resList == null ? new List<Student>() : resList;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student>? resList = (from g in _groups
                                      where g.CourseNumber.Equals(courseNumber)
                                      select g).FirstOrDefault()?.StudentsList;
            return resList == null ? new List<Student>() : resList;
        }

        public Student GetStudent(int id)
        {
            Student? student = (from g in _groups
                                from s in g.StudentsList
                                where s.Id.Equals(id)
                                select s).FirstOrDefault();
            if (student == null)
            {
                throw new NotExistStudentIdException();
            }

            return student;
        }
    }
}
