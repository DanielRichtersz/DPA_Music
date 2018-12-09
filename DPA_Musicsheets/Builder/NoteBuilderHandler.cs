using DPA_Musicsheets.Builder.Commands;
using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;

namespace DPA_Musicsheets.Builder
{
    class NoteBuilderHandler
    {
        private List<BuilderCommand> chain;
        private NoteBuilder noteBuilder;

        public NoteBuilderHandler()
        {
            chain = new List<BuilderCommand>();
            noteBuilder = new NoteBuilder();

            SetDurationCommand setDurationCommand = new SetDurationCommand(ref noteBuilder);
            SetMoleOrCrossCommand setMoleOrCrossCommand = new SetMoleOrCrossCommand(ref noteBuilder);
            SetOctaveCommand setOctaveCommand = new SetOctaveCommand(ref noteBuilder);
            SetPitchCommand setPitchCommand = new SetPitchCommand(ref noteBuilder);
            SetPointsCommand setPointsCommand = new SetPointsCommand(ref noteBuilder);
            SetTildeCommand setTildeCommand = new SetTildeCommand(ref noteBuilder);
            chain.Add(setDurationCommand);
            chain.Add(setMoleOrCrossCommand);
            chain.Add(setOctaveCommand);
            chain.Add(setPitchCommand);
            chain.Add(setPointsCommand);
            chain.Add(setTildeCommand);
        }

        public Note ExecuteChain(string chainParam)
        {
            for (int i = 0; 0 != chainParam.Length; ++i)
            {
                // Niet netjes/correct chain of responsibility
                if (chainParam[i] == 'e' || chainParam[i] == 'i')
                {
                    //If next char is s, execute setPitchCommand hardcoded
                    if (chainParam[i + 1] == 's')
                    {
                        chain[3].Execute(chainParam[i]);
                    }
                    //Skip the s
                    ++i;
                    //Dont loop further
                    continue;
                }
                
                // Chain of responsibility
                foreach (BuilderCommand bc in chain)
                {
                    if (bc.Execute(chainParam[i]))
                    {
                        break;
                    }
                }
            }
            Note note = noteBuilder.build();
            Console.WriteLine("Made note: " + note.ToString());
            return note;
        }
    }
}
