using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    class SetTildeCommand : BuilderCommand
    {
        public SetTildeCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(char c)
        {
            if (c == '~')
            {
                this.NoteBuilder.setTilde();
                return true;
            }
            return false;
        }
    }
}
