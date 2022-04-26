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
            this.model.Changed += (sender, eventargs) => this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (model != null) // When window is starting this is run, but the model didn't set!!
            {


                
                ImageBrush bg = new ImageBrush();
                  switch (model.ActualBGNumber)
                {
                    
                    case 1:
                        bg = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Backgrounds", "War1.png"), UriKind.RelativeOrAbsolute)));
                        break;
                    case 2:
                        bg = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Backgrounds", "War2.png"), UriKind.RelativeOrAbsolute)));
                        break;
                    case 3:
                        bg = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Backgrounds", "War3.png"), UriKind.RelativeOrAbsolute)));
                        break;
                    case 4:
                        bg = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Backgrounds", "War4.png"), UriKind.RelativeOrAbsolute)));
                        break;
                }

                drawingContext.DrawRectangle(bg, new Pen(Brushes.Black, 0), new Rect(0, 0, size.Width, size.Height));



                if (model.ActualMap[15,21] == MapLogic.Element.HOME)
                {
                    ImageBrush home = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Backgrounds", "home2.png"), UriKind.RelativeOrAbsolute)));
                    drawingContext.DrawRectangle(home, new Pen(Brushes.Black, 0), new Rect(750, 385, 400, 299));
                }
                
          
                BlocksLoad(drawingContext);
            }
        }

        private void BlocksLoad(DrawingContext drawingContext)
        {
            double w = model.ActualMap.GetLength(1);
            double h = model.ActualMap.GetLength(0);
            double rectWidth = size.Width / model.ActualMap.GetLength(1);
            double rectHeight = size.Height / model.ActualMap.GetLength(0);
            ;
            for (int i = 0; i < model.ActualMap.GetLength(0); i++)
            {
                for (int j = 0; j < model.ActualMap.GetLength(1); j++)
                {
                    ImageBrush brush = new ImageBrush();

                    switch (model.ActualMap[i, j])
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
                        case MapLogic.Element.NE:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "NE.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.PRE:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "PRE.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.CH:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "CH.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.CH1:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "CH1.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.F:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "F.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.G:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "G.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.H:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "H.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.EN:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "enemy.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.END:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "end.png"), UriKind.RelativeOrAbsolute)));
                            break;
                        case MapLogic.Element.HC:
                            brush = new ImageBrush
                                (new BitmapImage(new Uri(Path.Combine("Images", "Blocks", "HC.png"), UriKind.RelativeOrAbsolute)));
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
