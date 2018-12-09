using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    class SetDurationCommand : BuilderCommand
    {
        public SetDurationCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(char c)
        {
            if (char.IsDigit(c))
            {
                NoteBuilder.setDuration(c.ToString());
                return true;
            }
            return false;
        }
    }
}
