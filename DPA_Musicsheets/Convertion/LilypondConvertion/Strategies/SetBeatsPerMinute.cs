using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    class SetBeatsPerMinute : ILilypondStrategy
    {
        public void Execute(ref Track track, string stringPart)
        {
            int nTempo;
            int nBpm;
            bool nTempoConversion = int.TryParse(stringPart.Substring(0, stringPart.IndexOf("=")), out nTempo);
            bool nBpmConversion = int.TryParse(stringPart.Substring(stringPart.IndexOf("=") + 1), out nBpm);

            if (nTempoConversion && nBpmConversion)
            {
                track.SetBeatsPerMinute(nBpm);
                track.SetTempo(nTempo);
            }
        }
    }
}
