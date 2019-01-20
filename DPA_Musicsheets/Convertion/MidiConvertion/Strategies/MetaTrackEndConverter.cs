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
        public void Convert(MidiEvent midiEvent, ref Models.Track track)
        {

        }
    }
}
