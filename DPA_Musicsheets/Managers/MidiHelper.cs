using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Managers
{
    class MidiHelper
    {
        private Dictionary<int, Pitch> pitches = new Dictionary<int, Pitch>() {
            {0,Pitch.C}, {1,Pitch.C}, {2,Pitch.D}, {3,Pitch.D}, {4,Pitch.E}, {5,Pitch.F},
            {6,Pitch.F}, {7,Pitch.G}, {8,Pitch.G}, {9,Pitch.A}, {10,Pitch.A}, {11,Pitch.B} };
        private Dictionary<int, MoleOrCross> moles = new Dictionary<int, MoleOrCross>()
        {
            { 0, MoleOrCross.None }, { 1, MoleOrCross.Cross }, {2, MoleOrCross.None }, {3, MoleOrCross.Cross}, {4, MoleOrCross.None },
            { 5, MoleOrCross.None }, { 6, MoleOrCross.Cross}, {7, MoleOrCross.None }, {8, MoleOrCross.Cross},
            { 9, MoleOrCross.None }, {10, MoleOrCross.Cross}, {11, MoleOrCross.None }
        };
        
        public void GetPitch(int previousMidiKey, int midiKey, out Pitch pitch, out Octave octave, out MoleOrCross moc)
        {
            int key = midiKey % 12;

            //int octave = (midiKey / 12) - 1;
            pitch = pitches[key];
            moc = moles[key];

            // Check if note has distance more than 6 == Octave higher or lower
            int distance = (previousMidiKey != 0) ? midiKey - previousMidiKey : 0;

            int baseOctave = 0;
            while (distance < -6)
            {
                baseOctave++;
                distance += 8;
            }

            while (distance > 6)
            {
                baseOctave--;
                distance -= 8;
            }

            octave = (Octave)baseOctave;
        }

        public Duration getDuration(int absoluteTicks, int nextNoteAbsoluteTicks, int division,
            int beatNote, int beatsPerBar, out double percentageOfBar, out int dots)
        {
            Duration duration = 0;
            dots = 0;

            double deltaTicks = nextNoteAbsoluteTicks - absoluteTicks;

            if (deltaTicks <= 0)
            {
                percentageOfBar = 0;
                return Duration.None;
            }

            double percentageOfBeatNote = deltaTicks / division;
            percentageOfBar = (1.0 / beatsPerBar) * percentageOfBeatNote;

            for (int noteLength = 32; noteLength >= 1; noteLength -= 1)
            {
                double absoluteNoteLength = (1.0 / noteLength);

                if (percentageOfBar <= absoluteNoteLength)
                {
                    if (noteLength < 2)
                        noteLength = 2;

                    int subtractDuration;
                    
                    if (noteLength == 32)
                        subtractDuration = 32;
                    else if (noteLength >= 16)
                        subtractDuration = 16;
                    else if (noteLength >= 8)
                        subtractDuration = 8;
                    else if (noteLength >= 4)
                        subtractDuration = 4;
                    else
                        subtractDuration = 2;

                    if (noteLength >= 17)
                        duration = Duration.TweeEnDertig;
                    else if (noteLength >= 9)
                        duration = Duration.Zestiende;
                    else if (noteLength >= 5)
                        duration = Duration.Achste;
                    else if (noteLength >= 3)
                        duration = Duration.Kwart;
                    else
                        duration = Duration.Halve;

                    double currentTime = 0;

                    while (currentTime < (noteLength - subtractDuration))
                    {
                        var addtime = 1 / ((subtractDuration / beatNote) * Math.Pow(2, dots));
                        if (addtime <= 0) break;
                        currentTime += addtime;
                        if (currentTime <= (noteLength - subtractDuration))
                        {
                            dots++;
                        }
                        if (dots >= 4) break;
                    }

                    break;
                }
            }

            return duration;
        }
    }
}
