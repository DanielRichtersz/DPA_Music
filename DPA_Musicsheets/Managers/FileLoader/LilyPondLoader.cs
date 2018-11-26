using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Managers.FileLoader
{
	class LilyPondLoader : IFileLoader
	{
		public Track fileToTrack(string path)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var line in File.ReadAllLines(path))
			{
				sb.AppendLine(line);
			}

            string[] stringParts = stringToParts(sb.ToString());

            CreateTrackFromStringParts(stringParts);

            Track track = new Track(); ;
			return track;
		}

        private void CreateTrackFromStringParts(string[] stringParts)
        {
            Track track = new Track();

            //Reserved properties
            int playAmounts = 1;
            int startRepeat = 0;
            int endRepeat = 0;

            Tuple<int, int> time;
            int timeValue1 = 0;
            int timeValue2 = 0;


            for (int i = 0; i < stringParts.Length; ++i)
            {
                // Call strategies to convert string to objects
                // Reserved words
                if (stringParts[i] == "{")
                {

                }

                if (stringParts[i] == "/repeat")
                {
                    int.TryParse(stringParts[i + 2], out playAmounts);
                    startRepeat = i + 4;
                }
                if (stringParts[i] == "}" && startRepeat != 0 && endRepeat == 0)
                {
                    endRepeat = i;
                    // Encountered end of repeat
                    // Create a copy of the entire repeat
                    Staff repeatStaff = 
                }


            }
        }

        public string[] stringToParts(string lilyPondString)
        {
            return lilyPondString.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        }
	}


}
