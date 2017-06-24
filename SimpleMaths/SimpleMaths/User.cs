using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMaths
{
    public static class User
    {
        public static Difficulty difficulty { get; set; }
        public static Operations[] operations { get; set; }
        //public static string[] questions { get; set; }
        public static List<Questions> AdditionList { get; set; }
        public static List<Questions> SubtractionList { get; set; }
        public static List<Questions> MultiplyList { get; set; }
        public static List<Questions> DivideList { get; set; }


    }
}
