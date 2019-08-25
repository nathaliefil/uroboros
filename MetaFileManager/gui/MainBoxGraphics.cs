using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

namespace Uroboros.gui
{
    partial class MainForm : Form
    {

        private const int MIN_WINDOW_WIDTH = 600;
        private const int MIN_WINDOW_HEIGHT = 200;
        Style CommentStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        Style CardinalStyle = new TextStyle(Brushes.Black, null, FontStyle.Bold);
        Style UsualStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        Style InnerVariablesStyle = new TextStyle(Brushes.DarkViolet, null, FontStyle.Regular);


        private void CodeBoxSettings()
        {
            codeBox.AcceptsTab = true;
            codeBox.AutoIndent = true;
            codeBox.AutoIndentChars = false;
        }

        private void codeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // paint comments
            Range wideRange = (sender as FastColoredTextBox).VisibleRange;
            wideRange.ClearStyle(CommentStyle);
            wideRange.SetStyle(CommentStyle, @"//.*$", RegexOptions.Multiline);
            wideRange.SetStyle(CommentStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            wideRange.SetStyle(CommentStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline |
                        RegexOptions.RightToLeft);


            // paint cardinal keywords
            e.ChangedRange.ClearStyle(CardinalStyle);
            e.ChangedRange.SetStyle(CardinalStyle, @"\b(?i)(add|ask|clear bin|clear log|copy|create|cut|delete|
                |directory|drop|file|log off|log on|move|open|order|paste|print|reaccess|recreate|remodify|
                |remove|rename|reverse|run|select|sleep|swap)(?-i)\b");


            // paint usual keywords
            e.ChangedRange.ClearStyle(UsualStyle);
            e.ChangedRange.SetStyle(UsualStyle, @"\b(?i)(after|and|asc|before|by|desc|each|else|
                |first|for|force|from|if|in|inside|is|last|like|not|or|order by|skip|to|unique|where|while|with|
                |without|xor)(?-i)\b");


            // paint inner variables keywords, (true,false), time elements
            e.ChangedRange.ClearStyle(InnerVariablesStyle);
            e.ChangedRange.SetStyle(InnerVariablesStyle, @"\b(access|creation|directories|empty|exist|
                |extension|everything|files|fullname|index|iscorrect|isdirectory|isfile|location|modification|
                |name|now|size|this)\b");
            e.ChangedRange.SetStyle(InnerVariablesStyle, @"\b(?i)(false|true)(?-i)\b");
            e.ChangedRange.SetStyle(InnerVariablesStyle, @"\b(?i)(century|decade|year|month|weekday|day|hour|
                |minute|second|centuries|decades|years|months|days|hours|minutes|seconds)(?-i)\b");


            // todo: clear bin, clear log, log on, log off, uroboros stop, empty list, order by
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;

            codeBox.Height = 522 - 619 + control.Height;
            logBox.Height = 522 - 619 + control.Height;
            codeBox.Width = 730 - 1130 + control.Width;
            logBox.Left = 745 - 1130 + control.Width;
            locationBox.Width = 978 - 1130 + control.Width;
            directoryButton.Left = 1068 - 1130 + control.Width;

            // a lot of magic numbers
            /// to refactor

            if (this.Width < MIN_WINDOW_WIDTH)
                this.Width = MIN_WINDOW_WIDTH;

            if (this.Height < MIN_WINDOW_HEIGHT)
                this.Height = MIN_WINDOW_HEIGHT;
        }
    }
}
