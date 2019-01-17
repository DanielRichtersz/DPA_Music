using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Commands.EditorChain
{
    public class InsertClefLink : IEditorHotkeyLink
    {
        private IEditorHotkeyLink nextLink = new InsertTempoLink();

        public bool ProcessKey(EditorAction key)
        {
            if (key == EditorAction.AddClef)
            {
                var events = new ViewModelEvents();
                events.AddTextToEditor("\\clef treble");
                return true;
            }

            return nextLink.ProcessKey(key);
        }
    }
}
