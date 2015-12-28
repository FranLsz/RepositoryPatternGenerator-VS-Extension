using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepositoryPatternGenerator.Utils
{
    public static class WinFormsExtensions
    {
        public static void AppendLine(this RichTextBox source, string value, Color color)
        {
            source.SelectionStart = source.TextLength;
            source.SelectionLength = 0;

            source.SelectionColor = color;
            source.AppendLine(value);
            source.SelectionColor = source.ForeColor;
        }

        public static void AppendLine(this RichTextBox source, string value)
        {
            if (source.Text.Length == 0)
                source.Text = value;
            else
                source.AppendText("\r\n" + value);

            source.ScrollToCaret();
        }
    }
}
