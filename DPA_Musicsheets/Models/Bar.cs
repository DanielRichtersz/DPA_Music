using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
	class Bar : IStaffElement
	{
		private List<Note> Notes;
		private int beatsInBar;

		public Bar(int beatsInBar)
		{
			Notes = new List<Note>();
			this.beatsInBar = beatsInBar;
		}

        public List<Note> GetNotes()
        {
            return this.Notes;
        }

		public void addNote(Note n)
		{
			Notes.Add(n);
		}
	}
}
