using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_20212202_AYZ8R9.Renderer
{
    internal class Display: FrameworkElement
    {

        Size size;

        public void Resize(Size size)
        {
            this.size = size;
        }

        public int Counter = 0;

        public double Character_Pozition = 0;

        public double Last_Character_Pozition = 0;

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Counter++;
            drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", "Background.png"), UriKind.Relative)), new Rect(new Point(0, 0), new Point(size.Width, size.Height / 3 * 2.4)));
            drawingContext.DrawLine(new Pen(Brushes.Black, 3), new Point(0, size.Height/3*2.4), new Point(size.Width , size.Height / 3 * 2.4));
            if (Counter == 7)
            {
                Counter = 1;
            }
            System.Threading.Thread.Sleep(70);
            if (Last_Character_Pozition < Character_Pozition)
            {
                drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", "Main_Character", "Run", $"Run_{Counter}.png"), UriKind.Relative)), new Rect(new Point(Character_Pozition + 116, size.Height / 3 * 2.4 - 147), new Point(Character_Pozition, size.Height / 3 * 2.4)));
                Last_Character_Pozition = Character_Pozition;
            }
            else
            {
                drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", "Main_Character", "Run", $"Back_Run_{Counter}.png"), UriKind.Relative)), new Rect(new Point(Character_Pozition + 116, size.Height / 3 * 2.4 - 147), new Point(Character_Pozition, size.Height / 3 * 2.4)));
                Last_Character_Pozition = Character_Pozition;
            }
            
        }
    }
}
