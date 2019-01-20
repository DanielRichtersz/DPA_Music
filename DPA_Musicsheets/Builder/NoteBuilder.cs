using DPA_Musicsheets.Models;
using System;

namespace DPA_Musicsheets.Builder
{
    public class NoteBuilder
    {
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

        public void setMole(MoleOrCross mole)
        {
            note.moleOrCross = mole;
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
