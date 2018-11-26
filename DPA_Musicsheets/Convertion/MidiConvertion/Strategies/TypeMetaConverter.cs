﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Convertion.MidiConvertion.Strategies;
using DPA_Musicsheets.Models;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Convertion.MidiConvertion
{
    // Determine what MetaType the event is
    public class TypeMetaConverter : IMessageTypeConverter
    {
        private Dictionary<MetaType, IMetaTypeConverter> converters = new Dictionary<MetaType, IMetaTypeConverter>() {
            {MetaType.TimeSignature, new MetaTimeSignature() }, {MetaType.Tempo, new TempoMetaConverter() },
            {MetaType.EndOfTrack, new MetaTrackEndConverter() } };

        public void convert(MidiEvent midiEvent, ref Models.Track track)
        {
            MetaMessage metaMessage = midiEvent.MidiMessage as MetaMessage;
            MetaType metaType = metaMessage.MetaType;

            var converter = converters[metaType];

            converter.convert(midiEvent, ref track);

        }
    }
}
