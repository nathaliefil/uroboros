using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Uroboros.gui
{
    partial class MainForm : Form
    {
        private void RefreshCodeBoxGraphics()
        {
            ClearGraphics();

            foreach (string usualKeyword in HighlightKeywords.USUAL)
                this.CheckKeyword(usualKeyword, Color.Blue, 0, false);

            foreach (string cardinalKeyword in HighlightKeywords.CARDINAL)
                this.CheckKeyword(cardinalKeyword, Color.Black, 0, true);

            //foreach (string innerVariable in HighlightKeywords.INNER_VARIABLES)
            //    this.CheckKeyword(innerVariable, Color.DarkViolet, 0, false);

            PrintQuotations(Color.Maroon);

        }

        private void PrintQuotations(Color color)
        {
            int position = 0;
            bool quoted = false;
            int selectStart = codeBox.SelectionStart;

            for (int i = 0; i < codeBox.Text.Length; i++)
            {
                if (codeBox.Text[i].Equals('"'))
                {
                    if (quoted)
                    {
                        codeBox.Select(position, i - position + 1);
                        codeBox.SelectionColor = color;
                    }
                    else
                    {
                        position = i;
                    }
                    quoted = !quoted;
                }
            }
            if (quoted)
            {
                codeBox.Select(position, codeBox.Text.Length - position);
                codeBox.SelectionColor = color;
            }
            codeBox.Select(selectStart, 0);

        }

        private void ClearGraphics()
        {
            int selectionIndexStart = codeBox.SelectionStart;
            int selectionLength = codeBox.SelectionLength;

            codeBox.Select(0, codeBox.Text.Length);
            codeBox.SelectionColor = Color.Black;
            codeBox.SelectionFont = new Font(codeBox.Font, FontStyle.Regular);
            codeBox.Select(selectionIndexStart, selectionLength);
        }

        private void CheckKeyword(string word, Color color, int startIndex, bool bold)
        {
            List<int> indexes = AllIndexesOf(word);

            if (indexes.Count > 0)
            {
                int selectStart = codeBox.SelectionStart;

                foreach (int index in indexes)
                {
                    if (index != 0 && Char.IsLetter(codeBox.Text[index - 1]))
                        continue;

                    if (index != (codeBox.Text.Length - word.Length) && Char.IsLetter(codeBox.Text[index + word.Length]))
                        continue;

                    codeBox.Select((index + startIndex), word.Length);
                    codeBox.SelectionColor = color;
                    if (bold)
                        codeBox.SelectionFont = new Font(codeBox.Font, FontStyle.Bold);
                }
                codeBox.Select(selectStart, 0);
            }
        }

        public List<int> AllIndexesOf(string value)
        {
            if (String.IsNullOrEmpty(value))
                return new List<int>();
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = codeBox.Text.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}
