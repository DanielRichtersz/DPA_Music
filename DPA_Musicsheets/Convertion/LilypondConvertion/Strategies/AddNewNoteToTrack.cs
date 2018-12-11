using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Builder;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    class AddNewNoteToTrack : ILilypondStrategy
    {
        NoteBuilderHandler noteBuilderHandler = new NoteBuilderHandler();

        public void Execute(ref Track track, string stringPart)
        {
            Note newNote = noteBuilderHandler.ExecuteChain(stringPart);
            track.AddNote(newNote);
        }
    }
}
