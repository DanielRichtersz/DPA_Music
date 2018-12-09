using DPA_Musicsheets.Builder;
using DPA_Musicsheets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Convertion.LilypondConvertion
{
    class LilypondConverter
    {
        public void CreateTrackFromStringParts(string[] stringParts)
        {
            NoteBuilderHandler noteBuilderHandler = new NoteBuilderHandler();
            Track track = new Track();

            Tuple<int, int> time = new Tuple<int, int>(4, 4);
            int tempo = 0;
            int bpm = 0;

            //Add all bars and staffs to this staff
            Staff currentStaff = new Staff();
            Bar currentBar = new Bar(time);
            
            for (int i = 0; i < stringParts.Length; ++i)
            {
                // Call strategies to convert string to objects
                // Reserved words
                if (stringParts[i] == "{")
                {
                    //TODO CHECK
                    if (currentStaff.Bars.Count > 0)
                    {
                        track.AddStaff(currentStaff);
                        currentStaff = new Staff();
                    }
                }
                else if (stringParts[i] == "|")
                {
                    //TODO CHECK
                    if (currentBar.GetNotes().Count > 0)
                    {
                        currentStaff.Bars.Add(currentBar);
                        currentBar = new Bar(time);
                    }
                }
                else if (stringParts[i] == "}")
                {
                    //TODO CHECK
                    //End of previous bar/repeatance
                    currentStaff.Bars.Add(currentBar);
                    track.AddStaff(currentStaff);
                    currentStaff = new Staff();
                    currentBar = new Bar(time);

                    //Foreach note between start and endpoint, create note and add to bar

                    //For amount of repeats, add the bar to the lastAdded staff

                    // Encountered end of repeat
                    // Create a copy of the entire repeat
                    // Staff repeatStaff = new Staff(noteArray);
                }
                else if (stringParts[i] == "/clef")
                {
                    //TODO
                    // Niks mee doen volgens Rick
                }
                else if (stringParts[i] == "/time")
                {
                    //TODO CHECK
                    int timePartOne;
                    int timePartTwo;
                    bool timePartOneConversion = int.TryParse(stringParts[i + 1].Substring(0, stringParts[i + 1].IndexOf("/")), out timePartOne);
                    bool timePartTwoConversion = int.TryParse(stringParts[i + 1].Substring(stringParts[i + 1].IndexOf("/") + 1), out timePartTwo);

                    if (timePartOneConversion && timePartTwoConversion)
                    {
                        time = new Tuple<int, int>(timePartOne, timePartTwo);
                        currentBar.SetBeatsInBar(time);
                    }
                }
                else if (stringParts[i] == "/tempo")
                {
                    //TODO CHECK
                    int nTempo;
                    int nBpm;
                    bool nTempoConversion = int.TryParse(stringParts[i + 1].Substring(0, stringParts[i].IndexOf("=")), out nTempo);
                    bool nBpmConversion = int.TryParse(stringParts[i + 1].Substring(stringParts[i + 1].IndexOf("=") + 1), out nBpm);

                    if (nTempoConversion && nBpmConversion)
                    {
                        tempo = nTempo;
                        bpm = nBpm;
                    }
                    track.beatsPerMinute = bpm;
                }
                else
                {
                    //DONE
                    //Create note from string
                    string noteString = stringParts[i];
                    Note newNote = noteBuilderHandler.ExecuteChain(noteString);
                    currentBar.addNote(newNote);
                }
            }
        }
    }
}
