using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Managers.Components;
using DPA_Musicsheets.Models;
using PSAMControlLibrary;
using Note = PSAMControlLibrary.Note;

namespace DPA_Musicsheets.Managers
{
    class WpfStaffComponent
    {
        private List<IWpfDecorator> _components = new List<IWpfDecorator>();

        public void AddComponent(IWpfDecorator decorator)
        {
            _components.Add(decorator);
        }

        public NoteValues ExecuteAll(ref List<MusicalSymbol> symbols)
        {
            NoteValues noteValues = new NoteValues();
            
            foreach (var wpfComponent in _components)
            {
                
                wpfComponent.Execute(ref noteValues, ref symbols);

            }
            return noteValues;
        }
    }
}
