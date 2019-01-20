using DPA_Musicsheets.Models;
using System;

namespace DPA_Musicsheets.Builder
{
    public class NoteBuilder
    {
        private Note note = new Note(Pitch.R, Octave.small, MoleOrCross.None, Duration.Hele, 0, false);
        public Note Build()
        {
            Note outNote = note;
            note = new Note();

            return outNote;
        }

        public void SetPitch(Pitch pitch)
        {
            note.pitch = pitch;
        }

        public void IncreaseOctave()
        {
            if (note.octave != Octave.fourStriped)
            {
                note.octave++;
            }
        }

        public void DecreaseOctave()
        {
            if (note.octave != Octave.contra4)
            {
                note.octave--;
            }
        }

        public void SetOctave(Octave octave)
        {
            note.octave = octave;
        }

        public void SetMole(MoleOrCross mole)
        {
            note.moleOrCross = mole;
        }

        public void SetDuration(Duration duration)
        {
            note.duration = duration;
        }

        public void AddPoint()
        {
            note.points++;
        }

        public void SetPoints(int points)
        {
            note.points = points;
        }

        public void SetTilde()
        {
            note.hasTilde = true;
        }

    }
}
