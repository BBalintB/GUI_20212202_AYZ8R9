using GUI_20212202_AYZ8R9.Logic;
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
    public class CharacterDisplay : FrameworkElement
    {
        Size size;
        public void Resize(Size size)
        {
            this.size = size;
            this.InvalidateVisual();
        }

        ICharacter Character;

        public void SetupModel(ICharacter character)
        {
            this.Character = character;
            this.Character.Changed += (sender, eventargs) => this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            try
            {
                if (Character.DoingPath == "Back_Idle" || Character.DoingPath == "Idle")
                {
                    if (Character.Animation_Counter < 5)
                    {
                        drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", Character.CharacterType, $"{Character.DoingPath}_{Character.Animation_Counter}.png"), UriKind.Relative)), new Rect(new Point(Character.left_corner.Horizontal, Character.left_corner.Vertical), new Point(Character.right_corner.Horizontal, Character.right_corner.Vertical)));
                    }
                }
                else
                {
                    drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", Character.CharacterType, $"{Character.DoingPath}_{Character.Animation_Counter}.png"), UriKind.Relative)), new Rect(new Point(Character.left_corner.Horizontal, Character.left_corner.Vertical), new Point(Character.right_corner.Horizontal, Character.right_corner.Vertical)));
                }
            }
            catch (Exception)
            {
                drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", Character.CharacterType, $"{Character.DoingPath}_1.png"), UriKind.Relative)), new Rect(new Point(Character.left_corner.Horizontal, Character.left_corner.Vertical), new Point(Character.right_corner.Horizontal, Character.right_corner.Vertical)));
            }
        }
    }
}
