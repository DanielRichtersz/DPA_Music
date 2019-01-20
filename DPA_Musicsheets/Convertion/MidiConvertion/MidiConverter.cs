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

        private Models.Track domainTrack = new Models.Track();

        public Track ConvertMidiToStaff(Sequence sequence)
        {
            domainTrack.division = sequence.Division;

            //Get all tracks and loop through their events to create the staffs, bars and notes
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
                    channelConverter.Convert(mEvent, ref domainTrack);
                }
                else
                {
                    metaConverter.Convert(mEvent, ref domainTrack);
                }
            }
        }
    }
}