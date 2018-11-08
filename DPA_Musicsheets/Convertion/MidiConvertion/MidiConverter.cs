using DPA_Musicsheets.Managers;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Convertion.MidiConvertion
{
    class MidiConverter
    {

        string lilypondContentString;
        int _beatNote = 4;    // De waarde van een beatnote.
        int _bpm = 120;       // Aantal beatnotes per minute.
        int _beatsPerBar;     // Aantal beatnotes per maat.

        int previousMidiKey = 60; // Central C;
        int previousNoteAbsoluteTicks = 0;
        double percentageOfBarReached = 0;
        bool startedNoteIsClosed = true;
        int division;

        private StringBuilder lilypondContent;

        public void addTimeSignature(MidiEvent midiEvent)
        {
            var metaMessage = midiEvent.MidiMessage as MetaMessage;
            byte[] timeSignatureBytes = metaMessage.GetBytes();
            _beatNote = timeSignatureBytes[0];
            _beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));
            lilypondContent.AppendLine($"\\time {_beatNote}/{_beatsPerBar}");
        }

        public void addTempo(MidiEvent midiEvent)
        {
            var metaMessage = midiEvent.MidiMessage as MetaMessage;
            byte[] tempoBytes = metaMessage.GetBytes();
            int tempo = (tempoBytes[0] & 0xff) << 16 | (tempoBytes[1] & 0xff) << 8 | (tempoBytes[2] & 0xff);
            _bpm = 60000000 / tempo;
            lilypondContent.AppendLine($"\\tempo 4={_bpm}");
        }

        public void addEndOfTrack(MidiEvent midiEvent)
        {
            if (previousNoteAbsoluteTicks > 0)
            {
                // Finish the last notelength.
                double percentageOfBar;
                lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks
                    , midiEvent.AbsoluteTicks, division, _beatNote, _beatsPerBar, out percentageOfBar));

                lilypondContent.Append(" ");

                percentageOfBarReached += percentageOfBar;
                if (percentageOfBarReached >= 1)
                {
                    lilypondContent.AppendLine("|");
                    percentageOfBar = percentageOfBar - 1;
                }
            }
        }

        public void addChanel(MidiEvent midiEvent)
        {
            var channelMessage = midiEvent.MidiMessage as ChannelMessage;
            if (channelMessage.Command == ChannelCommand.NoteOn)
            {
                if (channelMessage.Data2 > 0) // Data2 = loudness
                {
                    // Append the new note.
                    lilypondContent.Append(MidiToLilyHelper.GetLilyNoteName(previousMidiKey, channelMessage.Data1));

                    previousMidiKey = channelMessage.Data1;
                    startedNoteIsClosed = false;
                }
                else if (!startedNoteIsClosed)
                {
                    // Finish the previous note with the length.
                    double percentageOfBar;
                    lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks,
                        division, _beatNote, _beatsPerBar, out percentageOfBar));
                    previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
                    lilypondContent.Append(" ");

                    percentageOfBarReached += percentageOfBar;
                    if (percentageOfBarReached >= 1)
                    {
                        lilypondContent.AppendLine("|");
                        percentageOfBarReached -= 1;
                    }
                    startedNoteIsClosed = true;
                }
                else
                {
                    lilypondContent.Append("r");
                }
            }

        }

    }
}