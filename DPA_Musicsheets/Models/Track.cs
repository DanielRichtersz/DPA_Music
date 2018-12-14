using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class Track
    {
        public int previousNoteAbsoluteTicks { get; set; }
        public int division { get; set; }
        public Tuple<int, int> defaultBeatsInBar { get; set; }

        private List<Staff> Staffs = new List<Staff>();
        private int tempBpm = 120;
        private int tempo = 4;

        public Track()
        {
            this.defaultBeatsInBar = new Tuple<int, int>(4, 4);
        }

        public void AddStaff(Tuple<int, int> beatsInBar)
        {
            Staff staff = new Staff();
            staff.Bars.Add(new Bar(beatsInBar));
            this.Staffs.Add(staff);
            defaultBeatsInBar = beatsInBar;
        }

        public void CreateNewStaff()
        {
            Staff staff = new Staff();

            if (tempBpm != 0)
            {
                staff.BeatsPerMinute = this.tempBpm;
            }
            staff.Bars.Add(new Bar(defaultBeatsInBar));
            Staffs.Add(staff);
        }

        internal void SetTempo(int nTempo)
        {
            this.tempo = nTempo;
        }

        public void AddNote(Note n)
        {
            // Get last bar and calculate if the length is already at 4
            Bar b = GetLastBar();
            int length = 0;

            if (b != null)
            {
                foreach (Note note in b.GetNotes())
                {
                    length += (1 / (int)note.GetNoteDuration());
                }

                // If the total length is the max length from \time or if addding the new note will surpass this length, create a new Bar
                if (length == this.defaultBeatsInBar.Item1 || length + (1 / n.GetNoteDuration()) > this.defaultBeatsInBar.Item1)
                {
                    this.addNewBar();
                    b = GetLastBar();
                }

                b.addNote(n);
            }
        }

        private Bar GetLastBar()
        {
            return (Bar)Staffs.Last().Bars.Last();
        }

        public void SetBeatsPerBar(Tuple<int, int> tuple)
        {
            Bar b = GetLastBar();
            b.SetBeatsInBar(tuple);
        }

        public void SetBeatsPerMinute(int bpm)
        {
            if (Staffs.Count > 0)
            {
                Staff s = Staffs.Last();
                s.BeatsPerMinute = bpm;
            }
            else
            {
                this.tempBpm = bpm;
            }
        }

        public void addNewBar()
        {
            Bar b = new Bar(defaultBeatsInBar);
            Staffs.Last().Bars.Add(b);
        }

        public void AddStaff(Staff staff)
        {
            this.Staffs.Add(staff);
            this.addNewBar();
        }

        public List<Staff> GetStaffs()
        {
            return Staffs;
        }

        public void print()
        {

            foreach (var s in Staffs)
            {

                Console.WriteLine('{');

                foreach (var b in s.Bars)
                {
                    Bar bar = (Bar)b;

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

                Console.WriteLine("}");
            }

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
