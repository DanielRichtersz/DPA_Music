using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Managers.Components
{
    public class NoteValues
    {
        public string NoteStep { get; set; }
        public int NoteAlter { get; set; }
        public int Octave { get; set; }
        public MusicalSymbolDuration Duration { get; set; }
        public NoteStemDirection StemDirection { get; set; }
        public NoteTieType TieType { get; set; }
        public List<NoteBeamType> BeamTypes { get; set; } = new List<NoteBeamType>() { NoteBeamType.Single };
        public int NumberOfDots { get; set; }
    }
}
