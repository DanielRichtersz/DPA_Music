using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models
{
    public class Track
    {
        private List<Staff> Staffs = new List<Staff>();
        public Tuple<int, int> defaultBeatsInBar { get; set; }
        public int beatsPerMinute { get; set; }
        public int previousNoteAbsoluteTicks { get; set; }
        public int division { get; set; }

        public Track()
        {

        }

        public void AddStaff(Tuple<int, int> beatsInBar)
        {
            Staff staff = new Staff();
            staff.Bars.Add(new Bar(beatsInBar));
            this.Staffs.Add(staff);
            defaultBeatsInBar = beatsInBar;
        }

        public void CreateNewStaff()
        {
            Staff staff = new Staff();
            staff.Bars.Add(new Bar(defaultBeatsInBar));
            Staffs.Add(staff);
        }

        public void AddStaff(Staff staff)
        {
            this.Staffs.Add(staff);
        }

        public List<Staff> GetStaffs()
        {
            return Staffs;
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
