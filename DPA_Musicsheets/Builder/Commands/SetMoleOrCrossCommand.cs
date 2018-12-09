using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    class SetMoleOrCrossCommand : BuilderCommand
    {
        public SetMoleOrCrossCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(char c)
        {
            if (c == 'e')
            {
                this.NoteBuilder.setMole(MoleOrCross.Mole);
                return true;
            }
            else if (c == 'i')
            {
                this.NoteBuilder.setMole(MoleOrCross.Cross);
                return true;
            }
            return false;
        }
    }
}
