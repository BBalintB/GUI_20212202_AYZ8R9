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
        IGameModel model;

        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);                      

            drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0),
                new Rect(0, 0, 1920, 1080));
            ImageBrush brush2 = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "Backgrounds", "Background.png"), UriKind.RelativeOrAbsolute)));
            drawingContext.DrawRectangle(brush2, new Pen(Brushes.Black, 0),new Rect(0,0,1920,1080));

            BlocksLoad(drawingContext);
                   
        }

        private void BlocksLoad(DrawingContext drawingContext)
        {
            double width = 1920;
            double hight = 1080;
            double rectWidth = width / model.GameMatrix.GetLength(1);
            double rectHeight = hight / model.GameMatrix.GetLength(0);
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

    //internal class Display: FrameworkElement
    //{
    //    Size size;

    //    public void Resize(Size size)
    //    {
    //        this.size = size;
    //    }

    //    public int Counter = 0;
    //    public int Counter2 = 0;

    //    public double Character_Pozition = 0;

    //    public double Last_Character_Pozition = 0;

    //    public bool jump = false;

    //    protected override void OnRender(DrawingContext drawingContext)
    //    {
    //        base.OnRender(drawingContext);
    //        Counter++;
    //        Counter2++;
    //        drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", "Background.png"), UriKind.Relative)), new Rect(new Point(0, 0), new Point(size.Width, size.Height / 3 * 2.4)));
    //        drawingContext.DrawLine(new Pen(Brushes.Black, 3), new Point(0, size.Height/3*2.4), new Point(size.Width , size.Height / 3 * 2.4));

    //        if (jump)
    //        {
    //            if (Counter == 7)
    //            {
    //                Counter = 1;
    //            }
    //            if (Counter2 >= 7)
    //            {
    //                Counter2 = 1;
    //            }
    //                drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", "Main_Character", "Run", $"Run_{Counter}.png"), UriKind.Relative)), new Rect(new Point(Character_Pozition + 116, size.Height / 3 * 2.4 - 147 -((-0.5*Counter2 * Counter2) + 3* Counter2)*10), new Point(Character_Pozition, size.Height / 3 * 2.4 - ((-0.5 * Counter2 * Counter2) + 3 * Counter2) * 10)));
    //                Last_Character_Pozition = Character_Pozition;
    //            jump = false;
    //        }
    //        else
    //        {
    //            if (Counter == 7)
    //            {
    //                Counter = 1;
    //            }
    //            System.Threading.Thread.Sleep(70);
    //            if (Last_Character_Pozition < Character_Pozition)
    //            {
    //                drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", "Main_Character", "Run", $"Run_{Counter}.png"), UriKind.Relative)), new Rect(new Point(Character_Pozition + 116, size.Height / 3 * 2.4 - 147), new Point(Character_Pozition, size.Height / 3 * 2.4)));
    //                Last_Character_Pozition = Character_Pozition;
    //            }
    //            else
    //            {
    //                drawingContext.DrawImage(new BitmapImage(new Uri(Path.Combine("Images", "Main_Character", "Run", $"Back_Run_{Counter}.png"), UriKind.Relative)), new Rect(new Point(Character_Pozition + 116, size.Height / 3 * 2.4 - 147), new Point(Character_Pozition, size.Height / 3 * 2.4)));
    //                Last_Character_Pozition = Character_Pozition;
    //            }
    //        }
    //    }
    }
}
