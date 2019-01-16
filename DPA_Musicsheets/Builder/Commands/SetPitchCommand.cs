using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetPitchCommand : BuilderCommand
    {
        public SetPitchCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            NoteBuilder = noteBuilder;
        }

        public override bool Execute(string s)
        {
            foreach (char c in s)
            {
                if (char.IsLetter(c))
                {
                    NoteBuilder.setPitch(c.ToString());
                    return true;
                }
            }
            return false;
        }
    }
}
