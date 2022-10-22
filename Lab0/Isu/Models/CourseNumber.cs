using System;

namespace Isu.Models
{
    public class CourseNumber
    {
        private const char CourseNumberCheck0 = '0';
        private const char CourseNumberCheck5 = '5';
        public CourseNumber(char number)
        {
            if (!IsAllowedCourse(number))
            {
                throw new Exception("Wrong course number");
            }
            else
            {
                Number = number;
            }
        }

        public char Number { get; }

        private static bool IsAllowedCourse(char number)
        {
            return number is < CourseNumberCheck5 and > CourseNumberCheck0;
        }
    }
}