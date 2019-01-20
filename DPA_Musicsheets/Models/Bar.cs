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
