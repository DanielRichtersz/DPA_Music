using DPA_Musicsheets.Convertion.MidiConvertion;
using DPA_Musicsheets.ViewModels;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Managers.FileLoader
{
	class MidiLoader : IFileLoader
	{
        

		public Models.Track fileToString(string path)
		{
		    Sequence midiSequence = new Sequence();
			midiSequence.Load(path);

            MidiConverter mc = new MidiConverter();
            
		    return mc.convertMidiToStaff(midiSequence);

        }
        


	}
}