using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsa15_lec3_List
{
    class Test
    {
        public string TestName { get; set; }
        public int CategoryId { get; set; }
        public List<Question> Questions { get; set; }
        public TimeSpan MaxTestTime { get; set; }
        public double PassMark { get; set; }
    }
}
