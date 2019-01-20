using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Commands.EditorChain
{
    public class InsertTime68Link : IEditorHotkeyLink
    {
        private IEditorHotkeyLink nextLink;

        public bool ProcessKey(EditorAction key)
        {
            if (key == EditorAction.Add68)
            {
                var events = new ViewModelEvents();
                events.AddTextToEditor("\\time 6/8");
                return true;
            }

            return false;
        }
    }
}
