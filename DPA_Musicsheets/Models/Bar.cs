﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
	public class Bar : IStaffElement
	{
		private List<Note> Notes;
		private Tuple<int, int> beatsInBar;

		public Bar(Tuple<int, int> beatsInBar)
		{
			Notes = new List<Note>();
			this.beatsInBar = beatsInBar;
		}

        public void SetBeatsInBar(Tuple<int, int> beatsInBar)
        {
            this.beatsInBar = beatsInBar;
        }

        public List<Note> GetNotes()
        {
            return this.Notes;
        }

		public void addNote(Note n)
		{
			Notes.Add(n);
            if (!this.CheckNoteDurations())
            {
                //TODO: Error
            }
		}

        public void print()
        {
            string line = "";

            foreach (var note in Notes)
            {

                line += note.pitch.ToString() + note.duration.ToString() + " ";

            }
            Console.WriteLine(line);
        }

        private bool CheckNoteDurations()
        {
            return false;
            //TODO
            //Tuple<int, int> totalDuration = new Tuple<0, 0>();
            //foreach (Note note in this.Notes)
            //{
            //    totalDuration += (1 / (int)note.duration);
            //}
            //return totalDuration == this.beatsInBar;
        }
    }
}
