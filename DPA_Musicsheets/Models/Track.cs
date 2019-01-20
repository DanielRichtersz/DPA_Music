using DPA_Musicsheets.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class Track
    {
        public int previousNoteAbsoluteTicks { get; set; }
        public int division { get; set; }
        public BarContext DefaultBarContext { get; set; }

        public List<Staff> Staffs { get; }
        public string defaultRelativeOctave { get ; private set; }
        public bool defaultRelativeOctaveHasBeenInitiated { get; private set; }
        public bool defaultTempoHasBeenInitiated { get; private set; }
        public bool defaultBpmHasBeenInitiated { get; private set; }
        public bool defaultBpbHasBeenInitiated { get; private set; }
        public bool defaultClefStyleHasBeenInitiated { get; private set; }

        public Track()
        {
            DefaultBarContext = new BarContext();
            defaultRelativeOctave = "c'";
            Staffs = new List<Staff>();
            Staffs.Add(new Staff(defaultRelativeOctave));
            Staffs.Last().RelativeOctave = defaultRelativeOctave;

            defaultRelativeOctaveHasBeenInitiated = false;
            defaultTempoHasBeenInitiated = false;
            defaultBpmHasBeenInitiated = false;
            defaultBpbHasBeenInitiated = false;
            defaultClefStyleHasBeenInitiated = false;
        }

        #region CreateNew
        public void CreateNewStaff()
        {
            Staff staff = new Staff(defaultRelativeOctave);
            Staffs.Add(staff);
        }

        public void CreateNewBar()
        {
            if (Staffs.Count == 0)
            {
                CreateNewStaff();
            }
            BarContext newBarContext = new BarContext();
            newBarContext.Tempo = DefaultBarContext.Tempo;
            newBarContext.BeatsInBar = DefaultBarContext.BeatsInBar;
            newBarContext.BeatsPerMinute = DefaultBarContext.BeatsPerMinute;
            newBarContext.ClefStyle = DefaultBarContext.ClefStyle;

            Bar b = new Bar(newBarContext);
            Staffs.Last().Bars.Add(b);
            Console.WriteLine("Made bar: " + b.ToString());
        }

        public void CreateNewNote(Note n)
        {
            // Get last bar and calculate if the length is already at 4
            Bar b = GetLastBar();
            double length = 0;

            if (b != null)
            {
                foreach (Note note in b.GetNotes())
                {
                    double noteDuration = note.GetNoteDuration();
                    length += (b.BarContext.BeatsInBar.Item2 / noteDuration);
                }
                Console.WriteLine("Length: " + length);

                // If the total length is the max length from \time or
                // if adding the new note will surpass this length
                // -> create a new Bar
                if (length + (b.BarContext.BeatsInBar.Item2 / n.GetNoteDuration()) > b.BarContext.BeatsInBar.Item1)
                {
                    CreateNewBar();
                    b = GetLastBar();
                }

                b.AddNote(n);
            }
        }
        #endregion CreateNew

        #region SetDefault
        private void SetDefaultRelativeOctave(string relativeOctave)
        {
            if (!defaultRelativeOctaveHasBeenInitiated)
            {
                defaultRelativeOctave = relativeOctave;
                defaultRelativeOctaveHasBeenInitiated = true;
            }
        }
        private void SetDefaultTempo(int nTempo)
        {
            if (!defaultTempoHasBeenInitiated)
            {
                DefaultBarContext.Tempo = nTempo;
                defaultTempoHasBeenInitiated = true;
            }
        }

        private void SetDefaultBeatsPerMinute(int bpm)
        {
            if (!defaultBpmHasBeenInitiated)
            {
                DefaultBarContext.BeatsPerMinute = bpm;
                defaultBpmHasBeenInitiated = true;
            }
        }

        private void SetDefaultBeatsPerBar(Tuple<int, int> bpb)
        {
            if (!defaultBpbHasBeenInitiated)
            {
                DefaultBarContext.BeatsInBar = bpb;
                defaultBpbHasBeenInitiated = true;
            }
        }

        private void SetDefaultClefStyle(string clefStyle)
        {
            if (!defaultClefStyleHasBeenInitiated)
            {
                DefaultBarContext.ClefStyle = clefStyle;
                defaultClefStyleHasBeenInitiated = true;
            }
        }
        #endregion SetDefault

        #region SetLast

        public void SetLastStaffRelativeOctave(string relativeOctave)
        {
            SetDefaultRelativeOctave(relativeOctave);
            Staff s = Staffs.Last();
            s.RelativeOctave = relativeOctave;
        }

        public void SetLastBarBeatsPerBar(Tuple<int, int> tuple)
        {
            SetDefaultBeatsPerBar(tuple);

            Bar b = GetLastBar();
            if (b != null)
            {
                b.BarContext.BeatsInBar = tuple;
            }
        }

        public void SetLastBarBeatsPerMinute(int bpm)
        {
            SetDefaultBeatsPerMinute(bpm);
            Bar b = GetLastBar();
            if (b != null)
            {
                b.BarContext.BeatsPerMinute = bpm;
            }
        }

        public void SetLastBarTempo(int tempo)
        {
            SetDefaultTempo(tempo);
            Bar b = GetLastBar();
            if (b != null)
            {
                b.BarContext.Tempo = tempo;
            }
        }

        public void SetLastBarClefStyle(string clefStyle)
        {
            SetDefaultClefStyle(clefStyle);
            Bar b = GetLastBar();
            if (b != null)
            {
                b.BarContext.ClefStyle = clefStyle;
            }
        }
        #endregion SetLast

        private Bar GetLastBar()
        {
            if (Staffs.Count == 0)
            {
                CreateNewStaff();
                CreateNewBar();
            }
            else if (Staffs.Last().Bars.Count == 0)
            {
                CreateNewBar();
            }
            return (Bar)Staffs.Last().Bars.Last();
        }

        public List<Staff> GetStaffs()
        {
            return Staffs;
        }

        public void Print()
        {
            TrackConverter tc = new TrackConverter();
            Console.Write(tc.ConvertToLilypondText(this));
        }
    }
}
