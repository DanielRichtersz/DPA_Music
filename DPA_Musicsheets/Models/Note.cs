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
    class Note // INote Hint v Docent
    {
        private Pitch pitch;
        private Octave octave;
        private MoleOrCross moleOrCross;
        private Duration duration;
        private bool hasPoint;
        private bool hasTilde;

        public Note(Pitch pitch, Octave octave, MoleOrCross moleOrCross, Duration duration, bool hasTilde)
        {
            this.pitch = pitch;
            this.octave = octave;
            this.moleOrCross = moleOrCross;
            this.duration = duration;
        }
    }
}
