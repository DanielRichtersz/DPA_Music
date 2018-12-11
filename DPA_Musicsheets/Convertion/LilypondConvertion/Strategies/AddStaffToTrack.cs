using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    class AddStaffToTrack : ILilypondStrategy
    {
        public void Execute(ref Track track, string stringPart)
        {
            track.AddStaff(new Staff());
        }
    }
}
