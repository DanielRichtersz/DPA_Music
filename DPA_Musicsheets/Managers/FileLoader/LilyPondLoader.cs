using DPA_Musicsheets.Convertion.LilypondConvertion;
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
        LilypondConverter lilypondConverter = new LilypondConverter();
		public string fileToString(string path)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var line in File.ReadAllLines(path))
			{
				sb.AppendLine(line);
			}

            string[] stringParts = stringToParts(sb.ToString());

            lilypondConverter.CreateTrackFromStringParts(stringParts);

            Track track = new Track(); ;
			return "string";
		}

        public string[] stringToParts(string lilyPondString)
        {
            return lilyPondString.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        }
	}
}
