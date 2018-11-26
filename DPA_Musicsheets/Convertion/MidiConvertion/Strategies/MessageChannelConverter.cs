using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Builder;
using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Models;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Convertion.MidiConvertion.Strategies
{
    // Tries to make a note from the event
    class MessageChannelConverter : IMessageTypeConverter
    {
        private int previousMidiKey;
        private bool startedNoteIsClosed;
        private double percentageOfBarReached;
        private NoteBuilder noteBuilder = new NoteBuilder();

        // Converts the event to note
        public void convert(MidiEvent midiEvent, ref Models.Track track)
        {
            var channelMessage = midiEvent.MidiMessage as ChannelMessage;
            if (channelMessage.Command == ChannelCommand.NoteOn)
            {
                if (channelMessage.Data2 > 0) // Data2 = loudness
                {
                    Pitch pitch;
                    Octave octave;

                    MidiHelper.GetPitch(previousMidiKey, channelMessage.Data1, out pitch, out octave);

                    noteBuilder.setPitch(pitch).setOctave(octave);

                    // Append the new note.
                    //string notePitch = MidiToLilyHelper.GetLilyNoteName(previousMidiKey, channelMessage.Data1);

                    previousMidiKey = channelMessage.Data1;
                    startedNoteIsClosed = false;
                }
                else if (!startedNoteIsClosed)
                {
                    var beats = track.defaultBeatsInBar;

                    // Finish the previous note with the length.
                    track.previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
                    
                    int dots = 0;
                    double percentageOfBar;

                    Duration duration = MidiHelper.getDuration(track.previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, track.division, 
                        beats.Item1, beats.Item2, out percentageOfBar, out dots);

                    noteBuilder.setDuration(duration).setPoints(dots);

                    percentageOfBarReached += percentageOfBar;
                    if (percentageOfBarReached >= 1)
                    {
                        //lilypondContent.AppendLine("|");
                        percentageOfBarReached -= 1;
                    }
                    startedNoteIsClosed = true;
                }
                else
                {
                    //lilypondContent.Append("r");
                    noteBuilder.setPitch(Pitch.R);
                }
            }
        }
    }
}
