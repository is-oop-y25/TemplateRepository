namespace Isu.Tools
{
    public class AddToFullGroupException : Exception
    {
        public AddToFullGroupException()
            : base("It is not possible to add a student to a full group")
        {
        }
    }
}
