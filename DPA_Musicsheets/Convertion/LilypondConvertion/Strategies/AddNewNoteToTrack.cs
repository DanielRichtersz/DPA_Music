using DPA_Musicsheets.Builder;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    public class AddNewNoteToTrack : ILilypondStrategy
    {
        NoteBuilderHandler noteBuilderHandler = new NoteBuilderHandler();

        public void Execute(ref Track track, ref int i, string stringPart)
        {
            Note newNote = noteBuilderHandler.ExecuteChain(stringPart);
            track.AddNote(newNote);
        }
    }
}
