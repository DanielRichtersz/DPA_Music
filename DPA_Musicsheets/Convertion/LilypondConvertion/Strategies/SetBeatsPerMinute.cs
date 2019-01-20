using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    public class SetBeatsPerMinute : ILilypondStrategy
    {
        public void Execute(ref Track track, ref int i, string stringPart)
        {
            int nTempo;
            int nBpm;

            bool containsIsSymbol = stringPart.Contains("=");

            if (containsIsSymbol)
            {
                bool nTempoConversion = int.TryParse(stringPart.Substring(0, stringPart.IndexOf("=")), out nTempo);
                bool nBpmConversion = int.TryParse(stringPart.Substring(stringPart.IndexOf("=") + 1), out nBpm);

                if (nTempoConversion && nBpmConversion)
                {
                    track.SetLastBarBeatsPerMinute(nBpm);
                    track.SetLastBarTempo(nTempo);
                }
            }

            ++i;
        }
    }
}
