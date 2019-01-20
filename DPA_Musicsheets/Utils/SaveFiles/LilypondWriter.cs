
using System.IO;

namespace DPA_Musicsheets.SaveFiles
{
    public class LilypondWriter : IFileWriter
    {
        public void WriteFile(string path, string text)
        {
            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.Write(text);
                outputFile.Close();
            }
        }
    }
}