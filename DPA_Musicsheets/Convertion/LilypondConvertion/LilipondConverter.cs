using DPA_Musicsheets.Builder;
using DPA_Musicsheets.Convertion.LilypondConvertion.Strategies;
using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Convertion.LilypondConvertion
{
    class LilypondConverter
    {
        Dictionary<string, ILilypondStrategy> lilypondStrategies = new Dictionary<string, ILilypondStrategy>();

        public LilypondConverter()
        {
            lilypondStrategies.Add("{", new AddStaffToTrack());
            lilypondStrategies.Add("|", new AddNewBarToTrack());
            lilypondStrategies.Add("/time", new SetBeatsPerBar());
            lilypondStrategies.Add("/tempo", new SetBeatsPerMinute());
            lilypondStrategies.Add("note", new SetBeatsPerMinute());
        }

        public Track CreateTrackFromStringParts(string[] stringParts)
        {
            Track track = new Track();

            for (int i = 0; i < stringParts.Length; ++i)
            {
                // Call strategies to convert string to objects
                try
                {
                    lilypondStrategies[stringParts[i]].Execute(ref track, stringParts[i + 1]);
                }
                catch (KeyNotFoundException knf)
                {
                    lilypondStrategies["note"].Execute(ref track, stringParts[i]);
                }
            }

            return track;
        }
    }
}
