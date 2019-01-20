using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetOctaveCommand : BuilderCommand
    {
        private readonly Dictionary<char, Func<bool>> chain = new Dictionary<char, Func<bool>>();

        public SetOctaveCommand(ref NoteBuilder noteBuilder, ref NoteBuilderResources noteBuilderResources) : base(ref noteBuilder, ref noteBuilderResources)
        {
            NoteBuilder = noteBuilder;
            NoteBuilderResources = noteBuilderResources;
            chain.Add('\'', increaseOctave);
            chain.Add(',', decreaseOctave);
        }

        public override bool Execute(string s)
        {
            foreach (char c in s)
            {
                bool containsKey = chain.TryGetValue(c, out var function);

                return containsKey && function();
            }
            return false;
        }

        private bool decreaseOctave()
        {
            NoteBuilder.DecreaseOctave();
            return true;
        }

        private bool increaseOctave()
        {
            NoteBuilder.IncreaseOctave();
            return true;
        }
    }
}
