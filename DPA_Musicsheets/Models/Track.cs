﻿using System;
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
            double length = 0;

            if (b != null)
            {
                foreach (Note note in b.GetNotes())
                {
                    double noteDuration = (double)note.GetNoteDuration();
                    length += (this.defaultBeatsInBar.Item2 / noteDuration);
                }
                Console.WriteLine("Length: " + length);

                // If the total length is the max length from \time or if addding the new note will surpass this length, create a new Bar
                if (length + (this.defaultBeatsInBar.Item2 / (double) n.GetNoteDuration()) > this.defaultBeatsInBar.Item1)
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
            Console.WriteLine("Made bar: " + b.ToString());
        }

        public void AddStaff()
        {
            Console.WriteLine("Adding staff...");
            Staff staff = new Staff();
            this.Staffs.Add(staff);
            this.addNewBar();
            Console.WriteLine("Added staff: " + staff);
        }

        public List<Staff> GetStaffs()
        {
            return Staffs;
        }

        public void print()
        {
            Console.Out.WriteLine(this);
        }

        public override string ToString()
        {
            String output = "";
            foreach (var s in Staffs)
            {

                output += '{';

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

                        output += notepitch;

                        if (n.octave == Octave.contra1)
                        {
                            output += ",";
                        }
                        if (n.octave == Octave.oneStriped)
                        {
                            output += "'";
                        }
                        int duration = (int)n.duration;
                        output += duration;

                        output += new string('.', n.points) + " ";

                    }

                    output += "|" + "\n";
                }

                output += "}";
            }

            return output;
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
