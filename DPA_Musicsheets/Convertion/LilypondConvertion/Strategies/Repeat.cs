using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    public class Repeat : ILilypondStrategy
    {
        public void Execute(ref Track track, ref int i, string stringPart)
        {
            i = i + 2;
        }
    }
}
