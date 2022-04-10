﻿using GUI_20212202_AYZ8R9.Logic;
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
    public class Display : FrameworkElement
    {
        public IGameModel model;
        Size size;

        public void Resize(Size size)
        {
            this.size = size;
        }

        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (model != null) // When window is starting this is run, but the model didn't set!!
            {
            //drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0),
            //    new Rect(0, 0, size.Width, size.Height));
            ImageBrush brush2 = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Backgrounds", "War2.png"), UriKind.RelativeOrAbsolute)));
            drawingContext.DrawRectangle(brush2, new Pen(Brushes.Black, 0),new Rect(0,0,1920,1080));

            //drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0),
            //new Rect(size.Width/2, size.Height/2, size.Width, size.Height));

            ImageBrush brush3 = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Backgrounds", "home2.png"), UriKind.RelativeOrAbsolute)));
            drawingContext.DrawRectangle(brush3, new Pen(Brushes.Black, 0), new Rect(800,485, 350, 196));

                BlocksLoad(drawingContext);
            }
        }

        private void BlocksLoad(DrawingContext drawingContext)
        {
            
            double rectWidth = size.Width / model.GameMatrix.GetLength(1);
            double rectHeight = size.Height / model.GameMatrix.GetLength(0);
            ;
            for (int i = 0; i < model.GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < model.GameMatrix.GetLength(1); j++)
                {
                    ImageBrush brush = new ImageBrush();

                    switch (model.GameMatrix[i, j])
                    {
                        case MapLogic.Element.A:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "A.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.B:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "B.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.C:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "C.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.D:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "D.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.E:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "E.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.R:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "R.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.S:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "S.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.V:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "V.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.U:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "U.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.W:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "W.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.Z:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "Z.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.Y:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "Y.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.L:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "L.png"), UriKind.RelativeOrAbsolute)));
                            break;
                    }

                    drawingContext.DrawRectangle(brush
                                , new Pen(Brushes.Black, 0),
                                new Rect(j * rectWidth, i * rectHeight, rectWidth, rectHeight)
                                );
                }
            }
        }
    }
}
