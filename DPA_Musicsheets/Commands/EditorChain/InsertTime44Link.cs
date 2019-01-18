using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Commands.EditorChain
{
    public class InsertTime44Link : IEditorHotkeyLink
    {
        private IEditorHotkeyLink nextLink = new InsertTime34Link();

        public bool ProcessKey(EditorAction key)
        {
            if (key == EditorAction.Add44)
            {
                var events = new ViewModelEvents();
                events.AddTextToEditor("\\time 4/4");
                return true;
            }

            return nextLink.ProcessKey(key);
        }
    }
}
