using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Convertion.MidiConvertion
{
    // Extracts the beatNote/beatsPerBar from the Event
    class MetaTimeSignature : IMetaTypeConverter
    {
        public void Convert(MidiEvent midiEvent, ref Models.Track track)
        {
            var metaMessage = midiEvent.MidiMessage as MetaMessage;

            byte[] timeSignatureBytes = metaMessage.GetBytes();
            int _beatNote = timeSignatureBytes[0];
            int _beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));

            track.SetLastBarBeatsPerBar(new Tuple<int, int>(_beatNote, _beatsPerBar));
        }
    }
}
