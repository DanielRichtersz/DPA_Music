using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder
{
    public class NoteBuilder
    {
        private Dictionary<string, Octave> octaves = new Dictionary<string, Octave>() {
            { ",", Octave.contra1 }, { "'", Octave.oneStriped}, { "", Octave.small}, { ",,", Octave.contra2 }, { "''", Octave.twoStriped}
            , { ",,,", Octave.contra3 }, { "'''", Octave.threeStriped}, { ",,,,", Octave.contra4 }, { "''''", Octave.fourStriped}};
        private Dictionary<string, MoleOrCross> moles = new Dictionary<string, MoleOrCross>() {
            { "es", MoleOrCross.Mole}, { "is", MoleOrCross.Cross}, { "", MoleOrCross.None} };
        private Dictionary<string, Duration> durations = new Dictionary<string, Duration>() {
            {"1", Duration.Hele },{"2", Duration.Halve },{"4", Duration.Kwart},{"8", Duration.Achste},{"16", Duration.Zestiende }
            ,{"32", Duration.TweeEnDertig} };

        private Note note = new Note(Pitch.R, Octave.small, MoleOrCross.None, Duration.Hele, 0, false);
        public Note build()
        {
            Note outNote = note;
            note = new Note();

            return outNote;
        }

        public void setPitch(string pitch)
        {
            Enum.TryParse<Pitch>(pitch.ToUpper(), out Pitch pEnum);
            note.pitch = pEnum;
        }

        public void setPitch(Pitch pitch)
        {
            note.pitch = pitch;
        }

        public void setOctave(string octave)
        {
            note.octave = octaves[octave];
        }

        public void increaseOctave()
        {
            if (note.octave != Octave.fourStriped)
            {
                note.octave++;
            }
        }

        public void decreaseOctave()
        {
            if (note.octave != Octave.contra4)
            {
                note.octave--;
            }
        }

        public void setOctave(Octave octave)
        {
            note.octave = octave;
        }

        public void setMole(string mole)
        {
            note.moleOrCross = moles[mole];
        }

        public void setMole(MoleOrCross mole)
        {
            note.moleOrCross = mole;
        }

        public void setDuration(string duration)
        {
            note.duration = durations[duration];
        }

        public void setDuration(Duration duration)
        {
            note.duration = duration;
        }

        public void addPoint()
        {
            note.points++;
        }

        public void setPoints(int points)
        {
            note.points = points;
        }

        public void setTilde()
        {
            note.hasTilde = true;
        }

    }
}
