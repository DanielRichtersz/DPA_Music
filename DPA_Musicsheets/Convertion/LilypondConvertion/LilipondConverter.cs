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
        public void CreateTrackFromStringParts(string[] stringParts)
        {
            Track track = new Track();

            Tuple<int, int> time = new Tuple<int, int>(4, 4);
            int tempo = 0;
            int bpm = 0;

            //Add all bars and staffs to this staff
            Staff root = new Staff();
            Staff lastAdded = new Staff();
            for (int i = 0; i < stringParts.Length; ++i)
            {
                // Call strategies to convert string to objects
                // Reserved words
                if (stringParts[i] == "{")
                {
                    lastAdded = new Staff();
                }
                else if (stringParts[i] == "}")
                {
                    //End of previous bar/repeatance
                    root.Bars.Add(lastAdded);

                    Bar bar = new Bar(time);
                    //Foreach note between start and endpoint, create note and add to bar

                    //For amount of repeats, add the bar to the lastAdded staff

                    // Encountered end of repeat
                    // Create a copy of the entire repeat
                    // Staff repeatStaff = new Staff(noteArray);
                }
                else if (stringParts[i] == "/time")
                {
                    int timePartOne;
                    int timePartTwo;
                    bool timePartOneConversion = int.TryParse(stringParts[i + 1].Substring(0, stringParts[i + 1].IndexOf("/")), out timePartOne);
                    bool timePartTwoConversion = int.TryParse(stringParts[i + 1].Substring(stringParts[i + 1].IndexOf("/") + 1, stringParts[i + 1].Length), out timePartTwo);

                    if (timePartOneConversion && timePartTwoConversion)
                    {
                        time = new Tuple<int, int>(timePartOne, timePartTwo);
                    }
                }
                else if (stringParts[i] == "/tempo")
                {
                    int nTempo;
                    int nBpm;
                    bool nTempoConversion = int.TryParse(stringParts[i + 1].Substring(0, stringParts[i].IndexOf("/")), out nTempo);
                    bool nBpmConversion = int.TryParse(stringParts[i + 1].Substring(stringParts[i + 1].IndexOf("/") + 1, stringParts[i + 1].Length), out nBpm);

                    if (nTempoConversion && nBpmConversion)
                    {
                        tempo = nTempo;
                        bpm = nBpm;
                    }
                }
                else
                {
                    //Create note from string

                }
            }
        }
    }
}
