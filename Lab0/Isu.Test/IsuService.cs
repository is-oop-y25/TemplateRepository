using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using Xunit;

namespace Isu.Test
{
    public class IsuService
    {
        [Fact]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            // declare group
            var groupName = new GroupName("M32001");
            Group newGroup = isuService.AddGroup(groupName);

            // declare student
            Student student = isuService.AddStudent(newGroup, "Petr Pervovich");
            Assert.Equal(groupName, student.GroupName);
            Assert.Equal(student, isuService.FindGroup(groupName).GetStudents()[0]);
        }

        [Fact]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            // declare group
            var groupName = new GroupName("M34406");
            Group newGroup = _isuService.AddGroup(groupName);

            // declare students
            _isuService.AddStudent(newGroup, "Aaron ronovich");
            _isuService.AddStudent(newGroup, "Baron Munch");
            _isuService.AddStudent(newGroup, "Speed Vagon");
            _isuService.AddStudent(newGroup, "Dagon, vladika mira");
            _isuService.AddStudent(newGroup, "Erema");
            _isuService.AddStudent(newGroup, "Yozhik");
            _isuService.AddStudent(newGroup, "Zheka Selskiyu");
            _isuService.AddStudent(newGroup, "Zadorny Molodec");
            _isuService.AddStudent(newGroup, "Iego");
            _isuService.AddStudent(newGroup, "Yesenin");
            Assert.Throws<IsuException>(() =>
            {
                _isuService.AddStudent(newGroup, "Koshkin Michail");
            });
        }

        [Fact]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Throws<IsuException>(() =>
            {
                // declare group
                var groupName = new GroupName("A37099");
            });
        }

        [Fact]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            // declare group
            var groupName1 = new GroupName("M3107");
            Group newGroup1 = _isuService.AddGroup(groupName1);
            var groupName2 = new GroupName("M3208");
            Group newGroup2 = _isuService.AddGroup(groupName2);

            // declare student
            Student student = _isuService.AddStudent(newGroup1, "Petr Pervovich");
            _isuService.ChangeStudentGroup(student, newGroup2);

            // Assert
            Assert.Equal(groupName2, student.GroupName);
            Assert.Equal(student, _isuService.FindGroup(groupName2).GetStudents()[0]);
        }
    }
}