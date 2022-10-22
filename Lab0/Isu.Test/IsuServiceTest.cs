using System;
using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test
{
    public class IsuServiceTest
    {
        private IIsuService _isuService = new IsuService();

        [Fact]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3207");
            Student name = _isuService.AddStudent(group, "Aleksey");

            Assert.True(name != null && name == _isuService.FindStudent(name.Id));
            Assert.Contains(name, _isuService.FindStudents("M3207"));
        }

        [Fact]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Throws<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3207");
                for (int i = 0; i < 25; i++)
                {
                    _isuService.AddStudent(group, "Aleksey");
                }
            });
        }

        [Fact]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Throws<IsuException>(() =>
            {
                _isuService.AddGroup("M32071");
            });
        }

        [Fact]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group7 = _isuService.AddGroup("M3207");
            Group group8 = _isuService.AddGroup("M3208");

            Student student1 = _isuService.AddStudent(group7, "Aleksey");
            Student student2 = _isuService.AddStudent(group8, "Boris");

            _isuService.ChangeStudentGroup(student2, group7);

            Assert.Contains(student1, _isuService.FindStudents("M3207"));
            Assert.True(!_isuService.FindStudents("M3208").Contains(student2));
        }
    }
}