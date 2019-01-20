using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    //Probably has:
    //Pitch: Enum of C, D, E, F, G, A, B
    //Octave
    //Has mole b or cross #
    public class Note // INote Hint v Docent
    {
        public Pitch pitch {get; set;}
        public Octave octave {get;set;}
        public MoleOrCross moleOrCross { get; set; }
        public Duration duration { get;  set; }
        public int points { get; set; }
        public bool hasTilde { get; set; }

        public Note() { }

        public double GetNoteDuration()
        {
            double totalDuration = (double)duration;

            if (points != 0)
            {
                for (int i = 1; i < points; ++i)
                {
                    totalDuration += totalDuration * (2 * i);
                }
            }

            return totalDuration;
        }

        public Note(Pitch pitch, Octave octave, MoleOrCross moleOrCross, Duration duration, int points, bool hasTilde)
        {
            this.pitch = pitch;
            this.octave = octave;
            this.moleOrCross = moleOrCross;
            this.duration = duration;
            this.hasTilde = hasTilde;
            this.points = points;
        }
        
    }
}
