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
    public class Display : FrameworkElement
    {
        IGameModel model;

        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            
            
                double rectWidth = 1920/ model.GameMatrix.GetLength(1);
                double rectHeight =1080 / model.GameMatrix.GetLength(0);

                drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0),
                    new Rect(0, 0, 1920, 1080));

                for (int i = 0; i < model.GameMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < model.GameMatrix.GetLength(1); j++)
                    {
                        ImageBrush brush = new ImageBrush();
                    
                        switch (model.GameMatrix[i, j])
                        {
                            case MapLogic.Element.A:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Images", "Blocks","A.jpg"), UriKind.RelativeOrAbsolute)));
                                break;
                            case MapLogic.Element.B:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "B.jpg"), UriKind.RelativeOrAbsolute)));
                                break;
                            case MapLogic.Element.C:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "C.jpg"), UriKind.RelativeOrAbsolute)));
                                break;
                            case MapLogic.Element.D:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "D.jpg"), UriKind.RelativeOrAbsolute)));
                                break;
                            case MapLogic.Element.E:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "E.jpg"), UriKind.RelativeOrAbsolute)));
                                break;
                            case MapLogic.Element.R:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "R.jpg"), UriKind.RelativeOrAbsolute)));
                            break;
                            case MapLogic.Element.S:
                                    brush = new ImageBrush
                                        (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "S.jpg"), UriKind.RelativeOrAbsolute)));
                                break;
                            case MapLogic.Element.V:
                                    brush = new ImageBrush
                                        (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "V.jpg"), UriKind.RelativeOrAbsolute)));
                                break;
                            case MapLogic.Element.U:
                                    brush = new ImageBrush
                                        (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "U.jpg"), UriKind.RelativeOrAbsolute)));
                                break;                           
                            case MapLogic.Element.W:
                                brush = new ImageBrush
                                    (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "W.jpg"), UriKind.RelativeOrAbsolute)));
                            break;

                        default:
                            brush = new ImageBrush
                                   (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "X.jpg"), UriKind.RelativeOrAbsolute)));
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
