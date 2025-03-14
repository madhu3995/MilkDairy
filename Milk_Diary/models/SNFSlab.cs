using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milk_Diary.models
{
    internal class SNFSlab
    {
        public int Id { get; set; }
        public double fromSNFValue { get; set; }
        public double toSNFValue { get; set; }
        public double Difference { get; set; }
    }
}
