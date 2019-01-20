using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DPA_Musicsheets.Factories;
using DPA_Musicsheets.Managers.FileLoader;
using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Models;
using Microsoft.Win32;

namespace DPA_Musicsheets.Utils
{
    // Loads the file loaders (MidiLoader, LilyPondLoader) to read files
	class FileHandler
	{

		private FileLoaderFactory fileLoaderFactory = new FileLoaderFactory();

		public FileHandler()
		{

			fileLoaderFactory.LoadFactory(".mid", new MidiLoader());
			fileLoaderFactory.LoadFactory(".ly", new LilyPondLoader());

		}

		public Track ReadFile(String path)
		{

			if (!IsMusicFile(path))
			{
				throw new NotSupportedException($"File extension {Path.GetExtension(path)} is not supported.");
			}

			IFileLoader loader = fileLoaderFactory.GetLoader(Path.GetExtension(path));

			return loader.FileToString(path);
		}

		public bool IsMusicFile(String filename)
		{

			string extension = Path.GetExtension(filename);

			return (extension.Equals(".mid") || extension.Equals(".ly"));

		}

	    public string SaveFileDialog(string extension = "*")
	    {
            SaveFileDialog fileDialog = new SaveFileDialog(){Filter = extension};

	        if (fileDialog.ShowDialog() == true)
	        {
	            return fileDialog.FileName;
	        }

	        return null;
	    }

	    public void SaveToPDF(string fileName, string text)
	    {
	        string withoutExtension = Path.GetFileNameWithoutExtension(fileName);
	        string tmpFileName = $"{fileName}-tmp.ly";
	        SaveToLilypond(tmpFileName, text);

	        string lilypondLocation = @"C:\Program Files (x86)\LilyPond\usr\bin\lilypond.exe";
	        string sourceFolder = Path.GetDirectoryName(tmpFileName);
	        string sourceFileName = Path.GetFileNameWithoutExtension(tmpFileName);
	        string targetFolder = Path.GetDirectoryName(fileName);
	        string targetFileName = Path.GetFileNameWithoutExtension(fileName);

	        var process = new Process
	        {
	            StartInfo =
	            {
	                WorkingDirectory = sourceFolder,
	                WindowStyle = ProcessWindowStyle.Hidden,
	                Arguments = String.Format("--pdf \"{0}\\{1}.ly\"", sourceFolder, sourceFileName),
	                FileName = lilypondLocation
	            }
	        };

	        process.Start();
	        while (!process.HasExited)
	        { /* Wait for exit */
	        }
	        if (sourceFolder != targetFolder || sourceFileName != targetFileName)
	        {
	            File.Move(sourceFolder + "\\" + sourceFileName + ".pdf", targetFolder + "\\" + targetFileName + ".pdf");
	            File.Delete(tmpFileName);
	        }
	    }

	    public void SaveToLilypond(string fileName, string text)
	    {
	        using (StreamWriter outputFile = new StreamWriter(fileName))
	        {
	            outputFile.Write(text);
	            outputFile.Close();
	        }
	    }
  

    }
}
