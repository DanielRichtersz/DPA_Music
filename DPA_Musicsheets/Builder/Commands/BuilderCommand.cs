using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public abstract class BuilderCommand
    {
        public BuilderCommand(ref NoteBuilder noteBuilder)
        {

        }

        public abstract bool Execute(string c);

        public NoteBuilder NoteBuilder { get; set; }
    }
}
