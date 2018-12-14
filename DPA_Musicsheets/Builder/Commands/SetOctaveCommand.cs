using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetOctaveCommand : BuilderCommand
    {
        public SetOctaveCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(string s)
        {
            foreach (char c in s)
            {
                if (c == '\'')
                {
                    this.NoteBuilder.increaseOctave();
                    return true;
                }
                if (c == ',')
                {
                    this.NoteBuilder.decreaseOctave();
                    return true;
                }
            }

            return false;
        }
    }
}
