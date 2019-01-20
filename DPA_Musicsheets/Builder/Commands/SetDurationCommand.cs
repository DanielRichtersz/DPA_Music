using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetDurationCommand : BuilderCommand
    {
        public SetDurationCommand(ref NoteBuilder noteBuilder, ref NoteBuilderResources noteBuilderResources) : base(ref noteBuilder, ref noteBuilderResources)
        {
            NoteBuilder = noteBuilder;
            NoteBuilderResources = noteBuilderResources;
        }

        public override bool Execute(string s)
        {
            int parseInt;
            bool parseSuccess = int.TryParse(s, out parseInt);
            if (!parseSuccess) return false;
            NoteBuilder.setDuration(NoteBuilderResources.GetDuration(s));
            return true;
        }
    }
}
