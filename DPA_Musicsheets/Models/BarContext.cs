using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class BarContext
    {
        public Tuple<int, int> BeatsInBar { get; set; }
        public int BeatsPerMinute { get; set; }
        public int Tempo { get; set; }
        public string ClefStyle { get; set; }



        public BarContext()
        {
            BeatsInBar = new Tuple<int, int>(4, 4);
            BeatsPerMinute = 120;
            Tempo = 4;
            ClefStyle = "treble";
        }

    }
}
