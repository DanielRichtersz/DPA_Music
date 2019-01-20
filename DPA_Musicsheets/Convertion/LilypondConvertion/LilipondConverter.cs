using DPA_Musicsheets.Builder;
using DPA_Musicsheets.Convertion.LilypondConvertion.Strategies;
using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Convertion.LilypondConvertion
{
    public class LilypondConverter
    {
        Dictionary<string, ILilypondStrategy> lilypondStrategies = new Dictionary<string, ILilypondStrategy>();

        public LilypondConverter()
        {
            lilypondStrategies.Add("{", new AddStaffToTrack());
            lilypondStrategies.Add("|", new AddNewBarToTrack());
            lilypondStrategies.Add(@"\time", new SetBeatsPerBar());
            lilypondStrategies.Add(@"\tempo", new SetBeatsPerMinute());
            lilypondStrategies.Add(@"\relative", new Relative());
            lilypondStrategies.Add(@"\clef", new Clef());
            lilypondStrategies.Add(@"\repeat", new Repeat());
            lilypondStrategies.Add(@"\alternative", new Alternative());
            lilypondStrategies.Add("}", new EndOfStaff());
            lilypondStrategies.Add("", new ZeroStrategy());
            lilypondStrategies.Add("note", new AddNewNoteToTrack());
        }

        public Track CreateTrackFromStringParts(string[] stringParts)
        {
            Track track = new Track();

            for (int i = 0; i < stringParts.Length - 1; ++i)
            {
                string stringPart = stringParts[i];
                // Call strategies to Convert string to objects
                if (lilypondStrategies.ContainsKey(stringParts[i]))
                {
                    lilypondStrategies[stringParts[i]].Execute(ref track, ref i, stringParts[i + 1]);
                }
                else
                {
                    lilypondStrategies["note"].Execute(ref track, ref i, stringParts[i]);
                }
            }

            CleanTrack(ref track);

            return track;
        }

        /*
         * Removes all empty bars and staffs
         */
        private void CleanTrack(ref Track track)
        {
            List<Staff> toBeRemoved = new List<Staff>();

            foreach (Staff staff in track.Staffs)
            {
                if (staff.Bars.Count == 0)
                {
                    toBeRemoved.Add(staff);
                }
                else
                {
                    List<Bar> toBeRemovedBars = new List<Bar>();
                    foreach (Bar bar in staff.Bars)
                    {
                        if (bar.GetNotes().Count == 0)
                        {
                            toBeRemovedBars.Add(bar);
                        }
                    }

                    foreach (Bar removeBar in toBeRemovedBars)
                    {
                        staff.Bars.Remove(removeBar);
                    }
                }
            }

            foreach (Staff removeStaff in toBeRemoved)
            {
                track.Staffs.Remove(removeStaff);
            }
        }
    }
}
