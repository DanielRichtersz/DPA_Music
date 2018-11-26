using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Convertion.MidiConvertion
{
    class TempoMetaConverter : IMetaTypeConverter
    {
        public void convert(MidiEvent midiEvent, ref Models.Track track)
        {
            var metaMessage = midiEvent.MidiMessage as MetaMessage;
            byte[] tempoBytes = metaMessage.GetBytes();
            int tempo = (tempoBytes[0] & 0xff) << 16 | (tempoBytes[1] & 0xff) << 8 | (tempoBytes[2] & 0xff);
            int _bpm = 60000000 / tempo;

            //lilypondContent.AppendLine($"\\tempo 4={_bpm}");

            track.beatsPerMinute = _bpm;
        }
    }
}
