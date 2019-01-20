using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
	public class Bar : IStaffElement
	{
        public BarContext BarContext { get; set; }

        private List<Note> Notes;

        public Bar(BarContext defaultBarContext)
		{
			Notes = new List<Note>();
            BarContext = defaultBarContext;
        }

        public List<Note> GetNotes()
        {
            return this.Notes;
        }

		public void AddNote(Note n)
		{
			Notes.Add(n);
		}

        public void Print()
        {
            string line = "";

            foreach (var note in Notes)
            {

                line += note.pitch.ToString() + note.duration.ToString() + " ";

            }
            Console.WriteLine(line);
        }
    }
}
