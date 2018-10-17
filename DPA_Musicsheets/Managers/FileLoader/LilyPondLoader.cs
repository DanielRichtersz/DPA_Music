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
		public string fileToString(string path)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var line in File.ReadAllLines(path))
			{
				sb.AppendLine(line);
			}

			return sb.ToString();
		}
	}
}
