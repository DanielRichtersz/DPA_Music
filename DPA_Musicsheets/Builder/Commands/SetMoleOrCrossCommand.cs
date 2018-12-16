using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetMoleOrCrossCommand : BuilderCommand
    {
        public SetMoleOrCrossCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(string s)
        {

                if (s == "es")
                {
                    this.NoteBuilder.setMole(MoleOrCross.Mole);
                    return true;
                }
                else if (s == "is")
                {
                    this.NoteBuilder.setMole(MoleOrCross.Cross);
                    return true;
                }
            this.NoteBuilder.setMole(MoleOrCross.None);

            return false;
        }
    }
}
