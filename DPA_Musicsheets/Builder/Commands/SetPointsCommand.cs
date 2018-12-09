using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    class SetPointsCommand : BuilderCommand
    {
        public SetPointsCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(char c)
        {
            if (c == '.')
            {
                this.NoteBuilder.addPoint();
                return true;
            }
            return false;
        }
    }
}
