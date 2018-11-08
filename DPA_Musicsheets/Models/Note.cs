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
        public Duration duration { get; set; }
        public bool hasPoint { get; set; }
        public bool hasTilde { get; set; }

        public Note() { }

        public Note(Pitch pitch, Octave octave, MoleOrCross moleOrCross, Duration duration, bool hasPoint, bool hasTilde)
        {
            this.pitch = pitch;
            this.octave = octave;
            this.moleOrCross = moleOrCross;
            this.duration = duration;
            this.hasTilde = hasTilde;
            this.hasPoint = hasPoint;
        }
    }
}
