
namespace DPA_Musicsheets.Memento
{
    public class History
    {
        private string _text;

        public string Text { get => _text; set => _text = value; }

        public History(string text)
        {
            _text = text;
        }
    }
}