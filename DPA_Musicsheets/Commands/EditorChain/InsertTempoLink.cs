using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Commands.EditorChain
{
    public class InsertTempoLink : IEditorHotkeyLink
    {
        private IEditorHotkeyLink nextLink = new InsertTime44Link();

        public bool ProcessKey(EditorAction key)
        {
            if (key == EditorAction.AddTempo)
            {
                var events = new ViewModelEvents();
                events.AddTextToEditor("\\tempo 4=120");
                return true;
            }

            return nextLink.ProcessKey(key);
        }
    }
}
