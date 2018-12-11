﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class Track
    {
        private List<Staff> Staffs = new List<Staff>();
        public Tuple<int, int> defaultBeatsInBar { get; set; }
        public int previousNoteAbsoluteTicks { get; set; }
        public int division { get; set; }

        public Track()
        {

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
            staff.Bars.Add(new Bar(defaultBeatsInBar));
            Staffs.Add(staff);
        }

        public void AddNote(Note n)
        {
            Bar b = (Bar)Staffs.Last().Bars.Last();
            b.addNote(n);
        }

        public void SetBeatsPerBar(Tuple<int, int> tuple)
        {
            Bar b = (Bar)Staffs.Last().Bars.Last();
            b.SetBeatsInBar(tuple);
        }

        public void SetBeatsPerMinute(int bpm)
        {
            Staff s = Staffs.Last();
            s.BeatsPerMinute = bpm;
        }

        public void addNewBar()
        {
            Bar b = new Bar(defaultBeatsInBar);
            Staffs.Last().Bars.Add(b);
        }

        public void AddStaff(Staff staff)
        {
            this.Staffs.Add(staff);
        }

        public List<Staff> GetStaffs()
        {
            return Staffs;
        }

        public void print()
        {

            foreach(var s in Staffs)
            {

                Console.WriteLine('{');

                foreach(var b in s.Bars)
                {
                    Bar bar = (Bar)b;

                    foreach(var n in bar.GetNotes())
                    {
                        string notepitch = n.pitch + "";
                        if(n.moleOrCross == MoleOrCross.Cross)
                        {
                            notepitch += "is";
                        }

                        Console.Write(notepitch);

                        if(n.octave == Octave.contra1)
                        {
                            Console.Write(",");
                        }
                        if(n.octave == Octave.oneStriped)
                        {
                            Console.Write("'");
                        }
                        int duration = (int) n.duration;
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
