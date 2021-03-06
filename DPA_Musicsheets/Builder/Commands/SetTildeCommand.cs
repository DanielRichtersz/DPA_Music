﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetTildeCommand : BuilderCommand
    {
        public SetTildeCommand(ref NoteBuilder noteBuilder, ref NoteBuilderResources noteBuilderResources) : base(ref noteBuilder, ref noteBuilderResources)
        {
            NoteBuilder = noteBuilder;
            NoteBuilderResources = noteBuilderResources;
        }

        public override bool Execute(string s)
        {
            foreach (char c in s)
            {
                if (c == '~')
                {
                    NoteBuilder.SetTilde();
                    return true;
                }
            }
            return false;
        }
    }
}
