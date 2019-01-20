using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    public class AddStaffToTrack : ILilypondStrategy
    {
        public void Execute(ref Track track, ref int i, string stringPart)
        {
            track.CreateNewStaff();
        }
    }
}
