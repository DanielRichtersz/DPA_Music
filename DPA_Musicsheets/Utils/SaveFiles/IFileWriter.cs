namespace DPA_Musicsheets.SaveFiles
{
    public interface IFileWriter
    {
        void WriteFile(string path, string text);
    }
}