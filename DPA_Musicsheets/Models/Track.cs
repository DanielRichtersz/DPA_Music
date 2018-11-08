﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class Track
    {
        private List<Bar> Bars;

        public Track()
        {

        }

        private void AddBar(Tuple<int, int> beatsInBar)
        {
            this.Bars.Add(new Bar(beatsInBar));
        }

        private void AddBar(Bar newBar)
        {
            this.Bars.Add(newBar);
        }

        //Relative
        //clef  (sleutel met daarachter treble, alt, tenor, of bass
        //{} om hele track?
        //tempo (4/bpm)
        //time (beatnote/beatsperbar)
        //repeat voor herhalingen
        //alternative bij herhalingen
        //Notes in juiste volgorde opslaan?

        //Maatsoort per track?

        //Herhalingen implementatie?

        //Events

    }
}
