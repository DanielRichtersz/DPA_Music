using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    class Bar
    {
        List<Note> Notes;
        int beatsInBar;

        public Bar(int beatsInBar)
        {
            Notes = new List<Note>();
            this.beatsInBar = beatsInBar;
        }
    }
}
