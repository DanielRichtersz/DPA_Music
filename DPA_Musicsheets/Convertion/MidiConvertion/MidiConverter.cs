using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Models;
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

        private Staff mainStaff = new Staff();

        public void convertMidiToStaff(Sequence sequence)
        {
            
            //get all tracks and loop through their events to create the staffs, bars and notes
            for(int i = 0; i < sequence.Count(); i++)
            {
                Sanford.Multimedia.Midi.Track track = sequence[i];

                foreach(var mEvent in track.Iterator())
                {



                }

            }

        }

        private StringBuilder lilypondContent;

        private void processMetaType(MidiEvent midiEvent)
        {

        }

    }
}