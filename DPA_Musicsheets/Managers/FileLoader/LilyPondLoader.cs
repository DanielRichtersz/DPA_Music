using DPA_Musicsheets.Convertion.LilypondConvertion;
using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;

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

			return null;
		}
	}
}
