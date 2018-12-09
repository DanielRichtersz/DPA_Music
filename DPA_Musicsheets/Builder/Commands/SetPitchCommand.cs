using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    class SetPitchCommand : BuilderCommand
    {
        public SetPitchCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(char c)
        {
            if (char.IsLetter(c))
            {
                this.NoteBuilder.setPitch(c.ToString());
                return true;
            }
            return false;
        }
    }
}
