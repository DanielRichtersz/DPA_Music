using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    public class Clef : ILilypondStrategy
    {
        public void Execute(ref Track track, ref int i, string stringPart)
        {
            track.SetLastBarClefStyle(stringPart);
            ++i;
        }
    }
}
