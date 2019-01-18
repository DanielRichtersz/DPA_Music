using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Commands.EditorChain
{
    public class InsertTime34Link : IEditorHotkeyLink
    {
        private IEditorHotkeyLink nextLink = new InsertTime68Link();

        public bool ProcessKey(EditorAction key)
        {
            if (key == EditorAction.Add34)
            {
                var events = new ViewModelEvents();
                events.AddTextToEditor("\\time 3/4");
                return true;
            }

            return nextLink.ProcessKey(key);
        }
    }
}
