using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    public interface ILilypondStrategy
    {
        void Execute(ref Track track, ref int i, string stringPart);
    }
}
