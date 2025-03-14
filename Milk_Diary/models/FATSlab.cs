using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milk_Diary.models
{
    internal class FATSlab
    {
        public int Id { get; set; }
        public double fromFatValue { get; set; }
        public double toFatValue { get; set; }
        public double Difference { get; set; }
    }
}
