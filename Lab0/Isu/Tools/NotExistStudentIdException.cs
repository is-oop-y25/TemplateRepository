namespace Isu.Tools
{
    public class NotExistStudentIdException : Exception
    {
        public NotExistStudentIdException()
            : base("The try to reach to not exist id")
        {
        }
    }
}
