using Isu.Tools;

namespace Isu.Models
{
    public class GroupName
    {
        public GroupName(string name)
        {
            if (name == null)
                throw new IsuException("string is null");

            if (name.Length != 6 || name[..2] != "M3" || !int.TryParse(name[3..6], out int _))
                throw new IsuException("The group name does not match the M3YXX pattern");

            try
            {
                Course = int.Parse(name[2..3]);
                Number = int.Parse(name[3..]);
                Name = name;
            }
            catch (Exception)
            {
                throw new IsuException("The group name does not match the M3YXX pattern");
            }
        }

        public int Course
        {
            get => Course;

            private init
            {
                if (value > 4 || value < 1)
                    throw new IsuException("Courses must be from 1st to 4th");
            }
        }

        public string Name { get; }
        public int Number { get; }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || GetType() != obj.GetType())
                return false;

            var groupName = (GroupName)obj;
            return Name == groupName.Name;
        }

        public override int GetHashCode() => HashCode.Combine(Name);
    }
}