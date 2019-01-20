using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Convertion.LilypondConvertion.Strategies
{
    public class SetBeatsPerBar : ILilypondStrategy
    {
        public void Execute(ref Track track, ref int i, string stringPart)
        {
            Tuple<int, int> time = new Tuple<int, int>(4, 4);

            int timePartOne;
            int timePartTwo;

            bool containsSlash = stringPart.Contains("/");

            if (containsSlash)
            {
                bool timePartOneConversion = int.TryParse(stringPart.Substring(0, stringPart.IndexOf("/")), out timePartOne);
                bool timePartTwoConversion = int.TryParse(stringPart.Substring(stringPart.IndexOf("/") + 1), out timePartTwo);

                if (timePartOneConversion && timePartTwoConversion)
                {
                    time = new Tuple<int, int>(timePartOne, timePartTwo);
                    track.SetLastBarBeatsPerBar(time);
                }
            }

            ++i;
        }
    }
}
