using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manager
{
    public static class Extensions
    {
        public static void ApplySettings(this Form form)
        {
            Settings settings = Form1.settings;
            ApplyFont(form, settings);
            ApplyBackground(form, settings);
        }
        private static void ApplyFont(Form form, Settings settings)
        {
            foreach (Control control in form.Controls)
                control.Font = new(settings.FontName, control.Font.Size);
        }
        private static void ApplyBackground(Form form, Settings settings)
        {
            Type[] badTypes = new Type[] { typeof(Button), typeof(ListView), typeof(TextBox), typeof(NumericUpDown) };
            foreach(object e in form.Controls)
            {
                if (badTypes.Contains(e.GetType()))
                    continue;
                try { (e as Control).BackColor = Color.Transparent; }
                catch { Form1.DisplayError(e.GetType().ToString()); }
            }
            if (File.Exists(settings.BackgroundLocation))
            {
                form.BackgroundImage = Image.FromFile(settings.BackgroundLocation);
                form.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (settings.BackgroundLocation != null)
                settings.BackgroundLocation = null;
        }
    }
}
