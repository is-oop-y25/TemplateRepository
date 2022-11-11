using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        // Arrange
        var isuSerivce = new IsuService();

        var groupName = new GroupName("A1105");

        // Act
        Group group = isuSerivce.AddGroup(groupName);
        Student student = isuSerivce.AddStudent(group, "Ivan");

        // Assert
        Assert.Equal(student.Group, group);
        Assert.Contains(student, group.StudentsList);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        // Arrange
        var isuSerivce = new IsuService();

        var groupName = new GroupName("A1105");

        // Act
        Group group = isuSerivce.AddGroup(groupName);

        // Assert
        Assert.Throws<AddToFullGroupException>(() =>
            {
                int maxVacnadePlaces = group.GetCountVacandePlaces();
                for (int i = 0; i <= maxVacnadePlaces + 1; i++)
                {
                    Student student = isuSerivce.AddStudent(group, $"Ivan{i}");
                }
            });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        // Arrange
        var isuSerivce = new IsuService();

        // Act

        // Assert
        Assert.Throws<InvalideGroupNameException>(() =>
            {
                var groupName = new GroupName("h1105");
                Group group = isuSerivce.AddGroup(groupName);
            });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        // Arrange
        var isuSerivce = new IsuService();

        var groupName = new GroupName("A1105");
        var groupName2 = new GroupName("A3105");

        // Act
        Group group = isuSerivce.AddGroup(groupName);
        Group group2 = isuSerivce.AddGroup(groupName2);

        Student student = isuSerivce.AddStudent(group, "Ivan");

        isuSerivce.ChangeStudentGroup(student, group2);

        // Assert
        Assert.Equal(student.Group, group2);
        Assert.Contains(student, group2.StudentsList);
    }
}