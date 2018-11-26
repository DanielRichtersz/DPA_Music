using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Convertion.MidiConvertion.Strategies
{
    class MetaTrackEndConverter : IMetaTypeConverter
    {
        // Write away as string. After refractor this is obsolete, will be done at printing the file
        public void convert(MidiEvent midiEvent, ref Models.Track track)
        {
            //What if i did absolutely nothing
            /*
            if (previousNoteAbsoluteTicks > 0)
            {
                // Finish the last notelength.
                double percentageOfBar;
                lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, _beatNote, _beatsPerBar, out percentageOfBar));
                lilypondContent.Append(" ");

                percentageOfBarReached += percentageOfBar;
                if (percentageOfBarReached >= 1)
                {
                    lilypondContent.AppendLine("|");
                    percentageOfBar = percentageOfBar - 1;
                }
            }
            */

        }
    }
}
