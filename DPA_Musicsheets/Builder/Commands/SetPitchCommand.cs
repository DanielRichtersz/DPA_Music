using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetPitchCommand : BuilderCommand
    {
        public SetPitchCommand(ref NoteBuilder noteBuilder, ref NoteBuilderResources noteBuilderResources) : base(ref noteBuilder, ref noteBuilderResources)
        {
            NoteBuilder = noteBuilder;
            NoteBuilderResources = noteBuilderResources;
        }

        public override bool Execute(string s)
        {
            foreach (char c in s)
            {
                if (char.IsLetter(c))
                {
                    NoteBuilder.SetPitch(NoteBuilderResources.GetPitch(c.ToString()));
                    return true;
                }
            }
            return false;
        }
    }
}
