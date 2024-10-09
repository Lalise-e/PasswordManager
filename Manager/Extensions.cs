using Password;
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
      public static MetaEntry settings { get; set; }
      private static Image backImage {  get; set; }
      public static void ApplySettings(this Form form)
      {
         if (string.IsNullOrEmpty(settings.BackgroundLocation))
            backImage = null;
         else if (!File.Exists(settings.BackgroundLocation))
            backImage = null;
         else
            backImage = Image.FromFile(settings.BackgroundLocation);
         ApplyFont(form);
         ApplyBackground(form);
      }
      private static void ApplyFont(Control form)
      {
         foreach (Control control in form.Controls)
         {
            control.Font = new(settings.FontName, control.Font.Size);
            if (control.Controls.Count != 0)
            {
               foreach(Control child in control.Controls)
               {
                  ApplyFont(child);
               }
            }
         }
      }
      private static void ApplyBackground(Control form)
      {
         form.BackgroundImage = backImage;
         form.BackgroundImageLayout = ImageLayout.Stretch;
         if(form.Controls.Count != 0)
         {
            foreach (Control child in form.Controls)
            {
               ApplyBackground(child);
            }
         }
      }
   }
}
