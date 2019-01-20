
using System.Collections.Generic;

namespace DPA_Musicsheets.Memento
{
    public class HistoryManager
    {
        private Stack<History> undoHistory = new Stack<History>();
        private Stack<History> redoHistory = new Stack<History>();

        public void AddUndoText(string text)
        {
            History newHistory = new History(text);
            undoHistory.Push(newHistory);
        }
        public void AddRedoText(string text)
        {
            History newHistory = new History(text);
            redoHistory.Push(newHistory);
        }

        public bool UndoAvailable()
        {
            return undoHistory.Count != 0;
        }

        public string GetLastUndoText()
        {
            var history = undoHistory.Pop();

            return history.Text;
        }

        public bool RedoAvailable()
        {
            return redoHistory.Count != 0;
        }

        public string GetLastRedoText()
        {
            var history = redoHistory.Pop();

            return history.Text;
        }

        public void ClearRedo()
        {
            redoHistory.Clear();
        }

    }
}