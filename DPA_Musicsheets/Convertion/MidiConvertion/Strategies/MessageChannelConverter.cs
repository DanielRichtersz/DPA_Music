using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Builder;
using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Models;
using Sanford.Multimedia.Midi;
using Track = DPA_Musicsheets.Models.Track;

namespace DPA_Musicsheets.Convertion.MidiConvertion.Strategies
{
    // Tries to make a note from the event
    class MessageChannelConverter : IMessageTypeConverter
    {
        private int previousMidiKey;
        private bool startedNoteIsClosed;
        private double percentageOfBarReached;
        private NoteBuilder noteBuilder = new NoteBuilder();
        private MidiHelper helper = new MidiHelper();

        // Converts the event to note
        public void Convert(MidiEvent midiEvent, ref Models.Track track)
        {
            var channelMessage = midiEvent.MidiMessage as ChannelMessage;
            if (channelMessage.Command == ChannelCommand.NoteOn)
            {
                if (channelMessage.Data2 > 0) // Data2 = loudness
                {
                    SetNoteSound(channelMessage);
                }
                else if (!startedNoteIsClosed)
                {
                    FinishNote(midiEvent, track);
                }
                else
                {
                    noteBuilder.SetPitch(Pitch.R);
                }
            }
        }

        private void FinishNote(MidiEvent midiEvent, Track track)
        {
            var beats = track.DefaultBarContext.BeatsInBar;

            Duration duration = helper.GetDuration(track.previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, track.division,
                beats.Item1, beats.Item2, out double percentageOfBar, out int dots);

            // Finish the previous note with the length.
            track.previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;

            noteBuilder.SetDuration(duration);
            noteBuilder.SetPoints(dots);
            var note = noteBuilder.Build();
            track.CreateNewNote(note);

            percentageOfBarReached += percentageOfBar;
            if (percentageOfBarReached >= 1)
            {
                track.CreateNewBar();
                percentageOfBarReached -= 1;
            }

            startedNoteIsClosed = true;
        }

        private void SetNoteSound(ChannelMessage channelMessage)
        {
            Pitch pitch;
            Octave octave;
            MoleOrCross mole;

            helper.GetPitch(previousMidiKey, channelMessage.Data1, out pitch, out octave, out mole);

            // Append the new note.
            noteBuilder = new NoteBuilder();
            noteBuilder.SetPitch(pitch);
            noteBuilder.SetOctave(octave);
            noteBuilder.SetMole(mole);

            previousMidiKey = channelMessage.Data1;
            startedNoteIsClosed = false;
        }
    }
}
