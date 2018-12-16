using DPA_Musicsheets.Builder.Commands;
using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;

namespace DPA_Musicsheets.Builder
{
    public class NoteBuilderHandler
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

        public Note ExecuteChain(string newNote)
        {
            for (int i = 0; i < newNote.Length; i++)
            {
                string subString = newNote.Substring(i, 1);
                int parseInt;
                bool isNumber = int.TryParse(newNote.Substring(i, 1), out parseInt);

                // Pitch
                if (newNote[i] == 'e' || newNote[i] == 'i')
                {
                    //If next char is s, execute setPitchCommand hardcoded
                    if (i != newNote.Length - 1 && newNote[i + 1] == 's')
                    {
                        subString = (newNote.Substring(i, 2));
                        i++;
                    }
                }
                else if (isNumber && i != newNote.Length - 1)
                {
                    isNumber = int.TryParse(newNote.Substring(i, 2), out parseInt);
                    if (isNumber)
                    {
                        subString = newNote.Substring(i, 2);
                        i++;
                    }
                }
                
                // Chain of responsibility
                foreach (BuilderCommand bc in chain)
                {
                    if (bc.Execute(subString))
                    {
                        break;
                    }
                }
            }
            Note note = noteBuilder.build();
            Console.Write("Made note: ");
            note.PrintString();
            return note;
        }
    }
}
