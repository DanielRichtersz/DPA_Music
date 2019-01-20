using System;
using System.Diagnostics;
using System.IO;

namespace DPA_Musicsheets.SaveFiles
{
    public class PDFWriter : IFileWriter
    {
        public void WriteFile(string path, string text)
        {
            string withoutExtension = Path.GetFileNameWithoutExtension(path);
            string tmpFileName = $"{path}-tmp.ly";
            new LilypondWriter().WriteFile(tmpFileName, text);

            string lilypondLocation = @"C:\Program Files (x86)\LilyPond\usr\bin\lilypond.exe";
            string sourceFolder = Path.GetDirectoryName(tmpFileName);
            string sourceFileName = Path.GetFileNameWithoutExtension(tmpFileName);
            string targetFolder = Path.GetDirectoryName(path);
            string targetFileName = Path.GetFileNameWithoutExtension(path);

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
    }
}