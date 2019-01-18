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
        public BarContext DefaultBarContext { get; }

        private List<Staff> Staffs;
        private string defaultRelativeOctave;
        private bool defaultRelativeOctaveHasBeenInitiated;
        private bool defaultTempoHasBeenInitiated;
        private bool defaultBpmHasBeenInitiated;
        private bool defaultBpbHasBeenInitiated;
        private bool defaultClefStyleHasBeenInitiated;

        public Track()
        {
            DefaultBarContext = new BarContext();
            defaultRelativeOctave = "c'";
            Staffs = new List<Staff>();
            Staffs.Add(new Staff(defaultRelativeOctave));
            Staffs.Last().relativeOctave = defaultRelativeOctave;

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

            Bar b = new Bar(DefaultBarContext);
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

                b.addNote(n);
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
            s.relativeOctave = relativeOctave;
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
            Bar returnBar;
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

        public void print()
        {
            bool firstBarPrinted = false;

            //Staff is empty
            if (Staffs.Count == 0)
            {
                Console.WriteLine("Track is empty");
                return;
            }

            Console.WriteLine("\\relative " + defaultRelativeOctave);

            foreach (var s in Staffs)
            {
                if (s.Bars.Count != 0)
                {
                    if (s.relativeOctave != defaultRelativeOctave)
                    {
                        Console.WriteLine("\\relative " + s.relativeOctave);
                    }

                    Console.WriteLine('{');

                    foreach (var b in s.Bars)
                    {
                        Bar bar = (Bar)b;

                        if (bar.GetNotes().Count != 0)
                        {

                            if (!firstBarPrinted)
                            {
                                Console.WriteLine("\\clef " + bar.BarContext.ClefStyle);
                                Console.WriteLine("\\time " + bar.BarContext.BeatsInBar.Item1 + "/" + bar.BarContext.BeatsInBar.Item2);
                                Console.WriteLine("\\tempo " + bar.BarContext.Tempo + "=" + bar.BarContext.BeatsPerMinute);
                                firstBarPrinted = true;
                            }
                            else
                            {
                                if (bar.BarContext.ClefStyle != DefaultBarContext.ClefStyle)
                                {
                                    Console.WriteLine("\\clef " + bar.BarContext.ClefStyle);
                                }

                                if (bar.BarContext.BeatsInBar.Item1 != DefaultBarContext.BeatsInBar.Item1
                                    || bar.BarContext.BeatsInBar.Item2 != DefaultBarContext.BeatsInBar.Item2)
                                {
                                    Console.WriteLine("\\time " + bar.BarContext.BeatsInBar.Item1 + "/" + bar.BarContext.BeatsInBar.Item2);
                                }

                                if (bar.BarContext.Tempo != DefaultBarContext.Tempo
                                    || bar.BarContext.BeatsPerMinute != DefaultBarContext.BeatsPerMinute)
                                {
                                    Console.WriteLine("\\tempo " + bar.BarContext.Tempo + "=" + bar.BarContext.BeatsPerMinute);
                                }
                            }

                            foreach (var n in bar.GetNotes())
                            {
                                string notepitch = n.pitch + "";

                                if (n.moleOrCross == MoleOrCross.Cross)
                                {
                                    notepitch += "is";
                                }

                                Console.Write(notepitch);

                                if (n.octave == Octave.contra1)
                                {
                                    Console.Write(",");
                                }
                                if (n.octave == Octave.oneStriped)
                                {
                                    Console.Write("'");
                                }
                                int duration = (int)n.duration;
                                Console.Write(duration);

                                Console.Write(new string('.', n.points) + " ");
                            }
                            Console.Write("|" + "\n");
                        }
                    }
                    Console.WriteLine("}");
                }
            }
        }

        public string toLilypond()
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool firstBarPrinted = false;

            //Staff is empty
            if (Staffs.Count == 0)
            {
                Console.WriteLine("Track is empty");
                return stringBuilder.ToString();
            }

            stringBuilder.AppendLine("\\relative " + defaultRelativeOctave);

            foreach (var s in Staffs)
            {
                if (s.Bars.Count != 0)
                {
                    if (s.relativeOctave != defaultRelativeOctave)
                    {
                        stringBuilder.AppendLine("\\relative " + s.relativeOctave);
                    }

                    stringBuilder.AppendLine("{");

                    foreach (var b in s.Bars)
                    {
                        Bar bar = (Bar)b;

                        if (bar.GetNotes().Count != 0)
                        {

                            if (!firstBarPrinted)
                            {
                                stringBuilder.AppendLine("\\clef " + bar.BarContext.ClefStyle);
                                stringBuilder.AppendLine("\\time " + bar.BarContext.BeatsInBar.Item1 + "/" + bar.BarContext.BeatsInBar.Item2);
                                stringBuilder.AppendLine("\\tempo " + bar.BarContext.Tempo + "=" + bar.BarContext.BeatsPerMinute);
                                firstBarPrinted = true;
                            }
                            else
                            {
                                if (bar.BarContext.ClefStyle != DefaultBarContext.ClefStyle)
                                {
                                    stringBuilder.AppendLine("\\clef " + bar.BarContext.ClefStyle);
                                }

                                if (bar.BarContext.BeatsInBar.Item1 != DefaultBarContext.BeatsInBar.Item1
                                    || bar.BarContext.BeatsInBar.Item2 != DefaultBarContext.BeatsInBar.Item2)
                                {
                                    stringBuilder.AppendLine("\\time " + bar.BarContext.BeatsInBar.Item1 + "/" + bar.BarContext.BeatsInBar.Item2);
                                }

                                if (bar.BarContext.Tempo != DefaultBarContext.Tempo
                                    || bar.BarContext.BeatsPerMinute != DefaultBarContext.BeatsPerMinute)
                                {
                                    stringBuilder.AppendLine("\\tempo " + bar.BarContext.Tempo + "=" + bar.BarContext.BeatsPerMinute);
                                }
                            }

                            foreach (var n in bar.GetNotes())
                            {
                                string notepitch = n.pitch + "";

                                if (n.moleOrCross == MoleOrCross.Cross)
                                {
                                    notepitch += "is";
                                }

                                stringBuilder.Append(notepitch);

                                if (n.octave == Octave.contra1)
                                {
                                    stringBuilder.Append(",");
                                }
                                if (n.octave == Octave.oneStriped)
                                {
                                    stringBuilder.Append("'");
                                }
                                int duration = (int)n.duration;
                                stringBuilder.Append(duration);

                                stringBuilder.Append(new string('.', n.points) + " ");
                            }
                            stringBuilder.Append("|" + "\n");
                        }
                    }
                    stringBuilder.AppendLine("}");
                }
            }

            return stringBuilder.ToString();
        }

        //Relative
        //clef  (sleutel met daarachter treble, alt, tenor, of bass
        //{} om hele track?
        //tempo (4/bpm)
        //time (beatnote/beatsperbar)
        //repeat voor herhalingen
        //alternative bij herhalingen
        //Notes in juiste volgorde opslaan?

        //Maatsoort per track?

        //Herhalingen implementatie?

        //Events

    }
}
