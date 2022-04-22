﻿using GUI_20212202_AYZ8R9.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_20212202_AYZ8R9.Renderer
{
    public class CharacterDisplay2 : FrameworkElement
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
            this.Character.Changed2 += (sender, eventargs) => this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);


            if (size.Width > 0 && size.Height > 0 && Character != null)
            {
                drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", "Main_Character", $"{Character.MainPath}", $"{Character.DoingPath}_{Character.Animation_Counter}.png"), UriKind.Relative)), new Rect(new Point(Character.left_corner.Horizontal, Character.left_corner.Vertical), new Point(Character.right_corner.Horizontal, Character.right_corner.Vertical)));
            }
        }

    }
}