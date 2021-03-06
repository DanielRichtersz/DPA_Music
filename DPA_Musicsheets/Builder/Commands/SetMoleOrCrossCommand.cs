﻿using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder.Commands
{
    public class SetMoleOrCrossCommand : BuilderCommand
    {
        public SetMoleOrCrossCommand(ref NoteBuilder noteBuilder, ref NoteBuilderResources noteBuilderResources) : base(ref noteBuilder, ref noteBuilderResources)
        {
            NoteBuilder = noteBuilder;
            NoteBuilderResources = noteBuilderResources;
        }

        public override bool Execute(string s)
        {
            //TODO
            if (s == "es")
            {
                NoteBuilder.SetMole(MoleOrCross.Mole);
                return true;
            }
            if (s == "is")
            {
                NoteBuilder.SetMole(MoleOrCross.Cross);
                return true;
            }
            NoteBuilder.SetMole(MoleOrCross.None);

            return false;
        }
    }
}
