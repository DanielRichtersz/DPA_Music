using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetDurationCommand : BuilderCommand
    {
        public SetDurationCommand(ref NoteBuilder noteBuilder) : base(ref noteBuilder)
        {
            this.NoteBuilder = noteBuilder;
        }

        public override bool Execute(string s)
        {
            int parseInt;
            bool parseSucces = int.TryParse(s, out parseInt);
            if (parseSucces)
            {
                NoteBuilder.setDuration(s);
                return true;
            }
            return false;
        }
    }
}
