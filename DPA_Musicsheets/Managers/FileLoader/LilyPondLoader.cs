using DPA_Musicsheets.Convertion.LilypondConvertion;
using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Managers.FileLoader
{
	class LilyPondLoader : IFileLoader
	{
        LilypondConverter lilypondConverter = new LilypondConverter();
        public Track fileToString(string path)
        {
            StringBuilder sb = new StringBuilder();

			foreach (var line in File.ReadAllLines(path))
			{
				sb.AppendLine(line);
			}

            string fullString = sb.ToString();
            string[] stringArray = Regex.Replace(fullString, @"\s+", " ").Split(' ');

			return lilypondConverter.CreateTrackFromStringParts(stringArray);
		}
	}
}
