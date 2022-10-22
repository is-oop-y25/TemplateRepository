namespace Isu.Entities
{
    public class Student
    {
        public Student(string name, int id, string groupName)
        {
            Name = name;
            Id = id;
            GroupName = groupName;
        }

        public string Name { get; }
        public int Id { get; }
        public string GroupName { get; set; }
    }
}