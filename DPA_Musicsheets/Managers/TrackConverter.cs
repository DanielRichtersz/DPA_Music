using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Managers.Components;
using DPA_Musicsheets.Models;
using DPA_Musicsheets.Utils;
using PSAMControlLibrary;
using Note = DPA_Musicsheets.Models.Note;

namespace DPA_Musicsheets.Managers
{
    public class TrackConverter
    {
        private FileHandler fileHandler = new FileHandler();

        public Track GetTrack(string path)
        {
            return fileHandler.ReadFile(path);
        }

        public List<MusicalSymbol> ConvertToMusicalSymbols(Track track)
        {
            //Note previousNote = new Note(0,0,0,0,0,false);
            List<MusicalSymbol> symbols = new List<MusicalSymbol>();

            symbols.Add(new Clef(ClefType.GClef, 2));
            symbols.Add(new TimeSignature(TimeSignatureType.Numbers, (UInt32)track.DefaultBarContext.BeatsInBar.Item1,
                (UInt32)track.DefaultBarContext.BeatsInBar.Item2));

            //tempo not supported
            //repeat not supported

            foreach (var staff in track.GetStaffs())
            {
                foreach (var bar in staff.GetBars())
                {
                    ConvertBar(ref symbols, bar);
                }
            }

            return symbols;
        }

        private void ConvertBar(ref List<MusicalSymbol> symbols, Bar bar)
        {
            symbols.Add(new Barline());

            foreach (var note in bar.GetNotes())
            {
                if (note.pitch == Pitch.R)
                {
                    symbols.Add(new Rest((MusicalSymbolDuration)note.duration));
                }
                else
                {
                    var psamNote = PsamNote(note, ref symbols);

                    symbols.Add(psamNote);
                }
            }
        }

        private PSAMControlLibrary.Note PsamNote(Note note, ref List<MusicalSymbol> symbols)
        {
            WpfStaffComponent component = new WpfStaffComponent();
            component.AddComponent(new PitchDecorator() { Pitch = note.pitch });
            component.AddComponent(new NoteDurationDecorator() { Length = (int)note.duration });
            component.AddComponent(new AlterDecorator() { MoleOrCross = note.moleOrCross });
            component.AddComponent(new DotsDecorator() { Dots = note.points });
            component.AddComponent(new OctaveDecorator() { Octave = note.octave });

            if (note.hasTilde)
            {
                component.AddComponent(new TildeDecorator());
            }

            NoteValues values = component.ExecuteAll(ref symbols);

            PSAMControlLibrary.Note psamNote = new PSAMControlLibrary.Note(values.NoteStep,
                values.NoteAlter, values.Octave, values.Duration, values.StemDirection,
                values.TieType,
                values.BeamTypes);
            psamNote.NumberOfDots = values.NumberOfDots;
            return psamNote;
        }

        public string ConvertToLilypondText(Track track)
        {

            StringBuilder stringBuilder = new StringBuilder();
            bool firstBarPrinted = false;
            
            if (track.Staffs.Count == 0)
            {
                Console.WriteLine("Track is empty");
                return stringBuilder.ToString();
            }

            stringBuilder.AppendLine("\\relative " + track.defaultRelativeOctave);

            foreach (var s in track.Staffs)
            {
                if (s.Bars.Count != 0)
                {
                    if (s.RelativeOctave != track.defaultRelativeOctave)
                    {
                        stringBuilder.AppendLine("\\relative " + s.RelativeOctave);
                    }

                    stringBuilder.AppendLine("{");

                    foreach (var b in s.Bars)
                    {
                        Bar bar = (Bar)b;
                        firstBarPrinted = AppendBar(track, stringBuilder, firstBarPrinted, bar);
                    }
                    stringBuilder.AppendLine("}");
                }
            }

            return stringBuilder.ToString();
        }

        private static bool AppendBar(Track track, StringBuilder stringBuilder, bool firstBarPrinted, Bar bar)
        {
            if (bar.GetNotes().Count != 0)
            {

                if (!firstBarPrinted)
                {
                    firstBarPrinted = SetFirstBarStyles(stringBuilder, bar);
                }
                else
                {
                    SetChangedStyles(track, stringBuilder, bar);
                }

                foreach (var n in bar.GetNotes())
                {
                    AddNote(stringBuilder, n);
                }
                stringBuilder.Append("|" + "\n");
            }

            return firstBarPrinted;
        }

        private static bool SetFirstBarStyles(StringBuilder stringBuilder, Bar bar)
        {
            bool firstBarPrinted;
            stringBuilder.AppendLine("\\clef " + bar.BarContext.ClefStyle);
            stringBuilder.AppendLine("\\time " + bar.BarContext.BeatsInBar.Item1 + "/" + bar.BarContext.BeatsInBar.Item2);
            stringBuilder.AppendLine("\\tempo " + bar.BarContext.Tempo + "=" + bar.BarContext.BeatsPerMinute);
            firstBarPrinted = true;
            return firstBarPrinted;
        }

        private static void SetChangedStyles(Track track, StringBuilder stringBuilder, Bar bar)
        {
            if (bar.BarContext.ClefStyle != track.DefaultBarContext.ClefStyle)
            {
                stringBuilder.AppendLine("\\clef " + bar.BarContext.ClefStyle);
            }

            if (bar.BarContext.BeatsInBar.Item1 != track.DefaultBarContext.BeatsInBar.Item1
                || bar.BarContext.BeatsInBar.Item2 != track.DefaultBarContext.BeatsInBar.Item2)
            {
                stringBuilder.AppendLine("\\time " + bar.BarContext.BeatsInBar.Item1 + "/" + bar.BarContext.BeatsInBar.Item2);
            }

            if (bar.BarContext.Tempo != track.DefaultBarContext.Tempo
                || bar.BarContext.BeatsPerMinute != track.DefaultBarContext.BeatsPerMinute)
            {
                stringBuilder.AppendLine("\\tempo " + bar.BarContext.Tempo + "=" + bar.BarContext.BeatsPerMinute);
            }
        }

        private static void AddNote(StringBuilder stringBuilder, Note n)
        {
            stringBuilder.Append(n.pitch);
            SetMoleOrCross(stringBuilder, n);
            SetOctave(stringBuilder, n);
            SetDuration(stringBuilder, n);

            stringBuilder.Append(new string('.', n.points) + " ");

            if (n.hasTilde)
            {
                stringBuilder.Append("~");
            }
        }

        private static void SetDuration(StringBuilder stringBuilder, Note n)
        {
            int duration = (int)n.duration;
            stringBuilder.Append(duration.ToString());
        }

        private static void SetOctave(StringBuilder stringBuilder, Note n)
        {
            int numberContras = (int)n.octave;
            if (numberContras > 0)
            {
                stringBuilder.Append(new string('\'', numberContras));
            }
            else
            {
                stringBuilder.Append(new string(',', numberContras * -1));
            }
        }

        private static void SetMoleOrCross(StringBuilder stringBuilder, Note n)
        {
            if (n.moleOrCross == MoleOrCross.Cross)
            {
                stringBuilder.Append("is");
            }
            if (n.moleOrCross == MoleOrCross.Mole)
            {
                stringBuilder.Append("es");
            }
        }
    }
}

