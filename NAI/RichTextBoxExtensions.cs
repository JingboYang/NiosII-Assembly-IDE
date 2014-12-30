using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace NAI
{

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionFont = GlobalVars.FONT;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void AppendTextBackColor(this RichTextBox box, string text, Color foreColor, Color backColor)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = foreColor;
            box.SelectionBackColor = backColor;
            box.SelectionFont = GlobalVars.FONT;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.SelectionBackColor = box.BackColor;
        }


    }
}
