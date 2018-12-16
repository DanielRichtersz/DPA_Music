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
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(string s)
        {
            foreach (char c in s)
            {
                if (c == 'e')
                {
                    Console.WriteLine("break");
                }
                if (char.IsLetter(c))
                {
                    this.NoteBuilder.setPitch(c.ToString());
                    return true;
                }
            }
            return false;
        }
    }
}
