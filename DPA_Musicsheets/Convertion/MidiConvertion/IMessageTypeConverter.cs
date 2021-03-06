﻿using DPA_Musicsheets.Models;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Convertion.MidiConvertion
{
    public interface IMessageTypeConverter
    {
        void Convert(MidiEvent midiEvent, ref Models.Track track);
    }
}
