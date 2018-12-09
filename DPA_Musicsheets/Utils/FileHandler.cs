using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DPA_Musicsheets.Factories;
using DPA_Musicsheets.Managers.FileLoader;
using DPA_Musicsheets.Managers;

namespace DPA_Musicsheets.Utils
{
    // Loads the file loaders (MidiLoader, LilyPondLoader) to read files
	class FileHandler
	{

		private FileLoaderFactory fileLoaderFactory = new FileLoaderFactory();

		public FileHandler()
		{

			fileLoaderFactory.loadFactory(".mid", new MidiLoader());
			fileLoaderFactory.loadFactory(".ly", new LilyPondLoader());

		}

		public String readFile(String path)
		{

			if (!isMusicFile(path))
			{
				throw new NotSupportedException($"File extension {Path.GetExtension(path)} is not supported.");
			}

			IFileLoader loader = fileLoaderFactory.getLoader(Path.GetExtension(path));

			return loader.fileToString(path);
		}

		public Boolean isMusicFile(String filename)
		{

			String extension = Path.GetExtension(filename);

			return (extension.Equals(".mid") || extension.Equals(".ly"));

		}

	}
}
