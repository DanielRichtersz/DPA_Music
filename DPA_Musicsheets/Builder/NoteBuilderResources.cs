using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;

namespace DPA_Musicsheets.Builder.Commands
{
    public class NoteBuilderResources
    {
        private Dictionary<string, Octave> octaves = new Dictionary<string, Octave>() {
            { ",", Octave.contra1 },
            { "'", Octave.oneStriped},
            { "", Octave.small},
            { ",,", Octave.contra2 },
            { "''", Octave.twoStriped},
            { ",,,", Octave.contra3 },
            { "'''", Octave.threeStriped},
            { ",,,,", Octave.contra4 },
            { "''''", Octave.fourStriped}

        };

        private Dictionary<string, MoleOrCross> moles = new Dictionary<string, MoleOrCross>() {
            { "es", MoleOrCross.Mole},
            { "is", MoleOrCross.Cross},
            { "", MoleOrCross.None}
        };

        private Dictionary<string, Duration> durations = new Dictionary<string, Duration>() {
            {"1", Duration.Hele },
            { "2", Duration.Halve },
            { "4", Duration.Kwart},
            { "8", Duration.Achste},
            { "16", Duration.Zestiende },
            { "32", Duration.TweeEnDertig}
        };

        public Octave GetOctave(string octave)
        {
            Octave returnOctave;
            bool exists = octaves.TryGetValue(octave, out returnOctave);
            if (exists)
            {
                return octaves[octave];
            }

            return Octave.small;
        }

        public MoleOrCross GetMole(string mole)
        {
            MoleOrCross returnMoleOrCross;
            bool exists = moles.TryGetValue(mole, out returnMoleOrCross);
            if (exists)
            {
                return returnMoleOrCross;
            }
            return MoleOrCross.None;
        }

        public Duration GetDuration(string duration)
        {
            Duration returnDuration;
            bool exists = durations.TryGetValue(duration, out returnDuration);
            if (exists)
            {
                return returnDuration;
            }

            return Duration.None;
        }
    }


}
