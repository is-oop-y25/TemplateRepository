using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;

namespace Isu.Entities
{
    public class Group
    {
        private const int GroupNameLength = 5;
        private const char CourseNumberCheck0 = '0';
        private const char CourseNumberCheck5 = '5';
        private const char FacultySymbolM = 'M';
        private const char FacultySymbol3 = '3';
        private const int MaxStudentsInGroup = 20;
        private List<Student> _studentsList;
        public Group(string name)
        {
            if (!IsAllowed(name))
            {
                throw new IsuException("Wrong name format");
            }

            GroupName = name;
            _studentsList = new List<Student>();
        }

        public string GroupName { get; }

        public void AddStudentToGroup(Student student)
        {
            if (_studentsList.Count < MaxStudentsInGroup)
            {
                _studentsList.Add(student);
            }
            else
            {
                throw new IsuException("Too many students");
            }
        }

        public void RemoveStudentFromGroup(Student student)
        {
            _studentsList.Remove(student);
        }

        public Student GetStudentWithId(int id)
        {
            return _studentsList.FirstOrDefault(curStudent => curStudent.Id == id);
        }

        public List<Student> GetStudentsList()
        {
            return _studentsList;
        }

        private static bool IsAllowed(string name)
        {
            int nameLength = name.Length;
            char letterSymbol = name[0];
            char courseNumber = name[2];

            return nameLength == GroupNameLength && courseNumber is < CourseNumberCheck5
                and > CourseNumberCheck0 && char.IsLetter(letterSymbol);
        }
    }
}