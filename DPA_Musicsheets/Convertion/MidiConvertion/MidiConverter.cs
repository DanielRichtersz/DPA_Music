using DPA_Musicsheets.Convertion.MidiConvertion.Strategies;
using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Models;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track = DPA_Musicsheets.Models.Track;

namespace DPA_Musicsheets.Convertion.MidiConvertion
{
    class MidiConverter
    {
        private MessageChannelConverter channelConverter = new MessageChannelConverter();
        private TypeMetaConverter metaConverter = new TypeMetaConverter();

        private Staff mainStaff = new Staff();
        private Models.Track domainTrack = new Models.Track();

        public Track convertMidiToStaff(Sequence sequence)
        {
            domainTrack.division = sequence.Division;
            //get all tracks and loop through their events to create the staffs, bars and notes
            for(int i = 0; i < sequence.Count(); i++)
            {
                Sanford.Multimedia.Midi.Track track = sequence[i];

                ConvertTrack(track);
            }
            return domainTrack;
        }

        private void ConvertTrack(Sanford.Multimedia.Midi.Track track)
        {
            foreach (var mEvent in track.Iterator())
            {
                if (mEvent.MidiMessage.MessageType == MessageType.Channel)
                {
                    channelConverter.convert(mEvent, ref domainTrack);
                }
                else
                {
                    metaConverter.convert(mEvent, ref domainTrack);
                }
            }
        }

        private void processMetaType(MidiEvent midiEvent)
        {

        }

    }
}