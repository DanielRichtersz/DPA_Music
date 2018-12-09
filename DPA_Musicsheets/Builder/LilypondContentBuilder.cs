using DPA_Musicsheets.Managers;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Builder
{
	class LilypondContent
	{
		string lilypondContentString;
		int _beatNote = 4;    // De waarde van een beatnote.
		int _bpm = 120;       // Aantal beatnotes per minute.
		int _beatsPerBar;     // Aantal beatnotes per maat.

		int previousMidiKey = 60; // Central C;
		int previousNoteAbsoluteTicks = 0;
		double percentageOfBarReached = 0;
		bool startedNoteIsClosed = true;
		int division;

		public class Builder
		{
			private StringBuilder lilypondContent;
			private LilypondContent lilypond;

			public Builder(int division)
			{
				lilypondContent = new StringBuilder();
				lilypondContent.AppendLine("\\relative c' {");
				lilypondContent.AppendLine("\\clef treble");
				this.lilypond.division = division;
			}

			public void addTimeSignature(MidiEvent midiEvent)
			{
				var metaMessage = midiEvent.MidiMessage as MetaMessage;
				byte[] timeSignatureBytes = metaMessage.GetBytes();
				lilypond._beatNote = timeSignatureBytes[0];
				lilypond._beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));
				lilypondContent.AppendLine($"\\time {lilypond._beatNote}/{lilypond._beatsPerBar}");
			}

			public void addTempo(MidiEvent midiEvent)
			{
				var metaMessage = midiEvent.MidiMessage as MetaMessage;
				byte[] tempoBytes = metaMessage.GetBytes();
				int tempo = (tempoBytes[0] & 0xff) << 16 | (tempoBytes[1] & 0xff) << 8 | (tempoBytes[2] & 0xff);
				lilypond._bpm = 60000000 / tempo;
				lilypondContent.AppendLine($"\\tempo 4={lilypond._bpm}");
			}

			public void addEndOfTrack(MidiEvent midiEvent)
			{
				if (lilypond.previousNoteAbsoluteTicks > 0)
				{
					// Finish the last notelength.
					double percentageOfBar;
					lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(lilypond.previousNoteAbsoluteTicks
						, midiEvent.AbsoluteTicks, lilypond.division, lilypond._beatNote, lilypond._beatsPerBar, out percentageOfBar));

					lilypondContent.Append(" ");

					lilypond.percentageOfBarReached += percentageOfBar;
					if (lilypond.percentageOfBarReached >= 1)
					{
						lilypondContent.AppendLine("|");
						percentageOfBar = percentageOfBar - 1;
					}
				}
			}

			public void addChanel(MidiEvent midiEvent)
			{
				var channelMessage = midiEvent.MidiMessage as ChannelMessage;
				if (channelMessage.Command == ChannelCommand.NoteOn)
				{
					if (channelMessage.Data2 > 0) // Data2 = loudness
					{
						// Append the new note.
						lilypondContent.Append(MidiToLilyHelper.GetLilyNoteName(lilypond.previousMidiKey, channelMessage.Data1));

						lilypond.previousMidiKey = channelMessage.Data1;
						lilypond.startedNoteIsClosed = false;
					}
					else if (!lilypond.startedNoteIsClosed)
					{
						// Finish the previous note with the length.
						double percentageOfBar;
						lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(lilypond.previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks,
							lilypond.division, lilypond._beatNote, lilypond._beatsPerBar, out percentageOfBar));
						lilypond.previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
						lilypondContent.Append(" ");

						lilypond.percentageOfBarReached += percentageOfBar;
						if (lilypond.percentageOfBarReached >= 1)
						{
							lilypondContent.AppendLine("|");
							lilypond.percentageOfBarReached -= 1;
						}
						lilypond.startedNoteIsClosed = true;
					}
					else
					{
						lilypondContent.Append("r");
					}
				}
			}

			public LilypondContent build()
			{
				lilypondContent.Append("}");
				lilypond.lilypondContentString = lilypondContent.ToString();

				return lilypond;
			}

		}
	}
}
