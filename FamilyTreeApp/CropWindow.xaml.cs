using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Convert;
using Image = System.Windows.Controls.Image;
using Point = System.Drawing.Point;
    
namespace FamilyTreeApp
{
    /// <summary>
    /// Interaction logic for CropWindow.xamly
    /// </summary>
    public partial class CropWindow : Window 
    {
        #region ctor
        
        public CropWindow(string srcImageRoute, object uploadImageCommandCaller)
        {
            InitializeComponent();

            if (uploadImageCommandCaller.GetType() == typeof(MemberControl)) Caller = uploadImageCommandCaller as MemberControl;
            else if (uploadImageCommandCaller.GetType() == typeof(MemberInfoControl)) Caller = uploadImageCommandCaller as MemberInfoControl;
            else if (uploadImageCommandCaller.GetType() == typeof(AddMemberControl)) Caller = uploadImageCommandCaller as AddMemberControl;

            ImageID++;
            if (LastCroppedImage == null) LastCroppedImage = new Image();

            using (var compressedImage = new Bitmap(694, 437))
            {
                var srcImage = new Bitmap(srcImageRoute);

                Graphics.FromImage(compressedImage).DrawImage(      // compress image to fit our Image control in view
                    srcImage,
                    new Point[] {
                    new Point(0, 0),
                    new Point(694, 0),
                    new Point(0, 437),
                    },
                    new System.Drawing.Rectangle(0, 0, ToInt32(srcImage.Width), ToInt32(srcImage.Height)),
                    GraphicsUnit.Pixel
                    );

                compressedImage.Save(@"..\..\tmp\_" + ImageID + ".png");    // save compressed image
            }

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri(new Uri(Environment.CurrentDirectory), @"..\tmp\_" + ImageID + ".png");
            bmp.EndInit();

            picture.Source = bmp;               // display our newly created compressed image in view
        }

        #endregion

        #region Properties            

        public static Image LastCroppedImage = null;
        private object Caller { get; }
        static private int ImageID { get; set; } = 0;

        #endregion

        #region Event handlers

        private void Thumb_LeftTopResize(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double vdel = resizeAreaCanvas.Height - e.VerticalChange;
            double hdel = resizeAreaCanvas.Width - e.HorizontalChange;

            vdel = (4 * hdel) / 3;

            if (vdel < 360 && hdel < 270
                && vdel > 60 && hdel > 45
                && Canvas.GetLeft(resizeAreaCanvas) - (hdel - resizeAreaCanvas.Width) > 4
                && Canvas.GetTop(resizeAreaCanvas) - (vdel - resizeAreaCanvas.Height) > 30) 
            {
                Canvas.SetLeft(resizeAreaCanvas, Canvas.GetLeft(resizeAreaCanvas) - (hdel - resizeAreaCanvas.Width));
                Canvas.SetTop(resizeAreaCanvas, Canvas.GetTop(resizeAreaCanvas) - (vdel - resizeAreaCanvas.Height));

                resizeAreaCanvas.Height = vdel;
                resizeAreaCanvas.Width = hdel;
            }
        }

        private void Thumb_RightTopResize(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double vdel = resizeAreaCanvas.Height - e.VerticalChange;
            double hdel = resizeAreaCanvas.Width + e.HorizontalChange;

            vdel = (4 * hdel) / 3;

            if (vdel < 360 && hdel < 270
                && vdel > 60 && hdel > 45
                && Canvas.GetTop(resizeAreaCanvas) - (vdel - resizeAreaCanvas.Height) > 30
                && Canvas.GetLeft(resizeAreaCanvas) + hdel < canvas.Width - 18)
            {
                Canvas.SetTop(resizeAreaCanvas, Canvas.GetTop(resizeAreaCanvas) - (vdel - resizeAreaCanvas.Height));

                resizeAreaCanvas.Height = vdel;
                resizeAreaCanvas.Width = hdel;
            }
        }

        private void Thumb_RightBottomResize(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double vdel = resizeAreaCanvas.Height + e.VerticalChange;
            double hdel = resizeAreaCanvas.Width + e.HorizontalChange;

            vdel = (4 * hdel) / 3;

            if (vdel < 360 && hdel < 270
                && vdel > 60 && hdel > 45
                && Canvas.GetTop(resizeAreaCanvas) + vdel < canvas.Height - 18
                && Canvas.GetLeft(resizeAreaCanvas) + hdel < canvas.Width - 18)
            {
                resizeAreaCanvas.Height = vdel;
                resizeAreaCanvas.Width = hdel;
            }
        }

        private void Thumb_LeftBottomResize(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            double vdel = resizeAreaCanvas.Height + e.VerticalChange;
            double hdel = resizeAreaCanvas.Width - e.HorizontalChange;

            vdel = (4 * hdel) / 3;

            if (vdel < 360 && hdel < 270
                && vdel > 60 && hdel > 45
                && Canvas.GetTop(resizeAreaCanvas) + vdel < canvas.Height - 18
                && Canvas.GetLeft(resizeAreaCanvas) > 2)
            {
                Canvas.SetLeft(resizeAreaCanvas, Canvas.GetLeft(resizeAreaCanvas) - (hdel - resizeAreaCanvas.Width));

                resizeAreaCanvas.Height = vdel;
                resizeAreaCanvas.Width = hdel;
            }
        }

        private void Thumb_Move(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var location = resizeAreaCanvas.PointToScreen(new System.Windows.Point(0.0, 0.0));
            var locationCanvas = canvas.PointToScreen(new System.Windows.Point(0.0, 0.0));
            
            //check left/right border
            if (location.X + e.HorizontalChange < locationCanvas.X + canvas.Width - resizeAreaCanvas.Width - 18
                && location.X + e.HorizontalChange > locationCanvas.X + 2)
                Canvas.SetLeft(resizeAreaCanvas, Canvas.GetLeft(resizeAreaCanvas) + e.HorizontalChange);

            //check top/bottom border
            if (location.Y + e.VerticalChange > locationCanvas.Y + 26
                && location.Y + e.VerticalChange < locationCanvas.Y + canvas.Height - resizeAreaCanvas.Height - 14)
                Canvas.SetTop(resizeAreaCanvas, Canvas.GetTop(resizeAreaCanvas) + e.VerticalChange);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var canvasLocation= canvas.PointToScreen(new System.Windows.Point(0.0, 0.0));
            var resizeAreaLocation = resizeAreaCanvas.PointToScreen(new System.Windows.Point(0.0, 0.0));

            // Cut out our area from the image and save it into new file
            using (var target = new Bitmap(Convert.ToInt32(resizeAreaCanvas.Width), Convert.ToInt32(resizeAreaCanvas.Height)))
            {
                Graphics.FromImage(target).DrawImage(
                    new Bitmap(System.Web.HttpUtility.UrlDecode(picture.Source.ToString()).Remove(0, 8)),
                        new Point[] {
                        new Point(0,0),
                        new Point(ToInt32(resizeAreaCanvas.Width), 0),
                        new Point(0, ToInt32(resizeAreaCanvas.Height))
                    },
                    new System.Drawing.Rectangle(
                        ToInt32(resizeAreaLocation.X - canvasLocation.X), 
                        ToInt32(resizeAreaLocation.Y - canvasLocation.Y - 26), 
                        ToInt32(resizeAreaCanvas.Width), 
                        ToInt32(resizeAreaCanvas.Height)
                        ),
                    GraphicsUnit.Pixel
                    );
                target.Save(@"..\..\tmp\" + ImageID + ".png");
            }
            
            // Fit our cutted image to MemberInfoControl picture size
            FitCroppedImage(@"..\..\tmp\" + ImageID + ".png");

            this.DialogResult = true;
            this.Close();
        }

        #endregion

        #region Auxiliary methods

        private void FitCroppedImage(string path)
        {
            int targetWidth = 0;
            int targetHeight = 0;

            if (Caller.GetType() == typeof(MemberControl)) { targetWidth = ToInt32((Caller as MemberControl).memberFacePic.Width); targetHeight = ToInt32((Caller as MemberControl).memberFacePic.Height); }
            else if (Caller.GetType() == typeof(MemberInfoControl)) { targetWidth = ToInt32((Caller as MemberInfoControl).memberFacePic.Width); targetHeight = ToInt32((Caller as MemberInfoControl).memberFacePic.Height); }
            else if (Caller.GetType() == typeof(AddMemberControl)) { targetWidth = ToInt32((Caller as AddMemberControl).memberFacePic.Width); targetHeight = ToInt32((Caller as AddMemberControl).memberFacePic.Height); }

            var compressedImage = new Bitmap(targetWidth, targetHeight);
            using (var srcImage = new Bitmap(path))
            {
                Graphics.FromImage(compressedImage).DrawImage(      // compress image to fit our Image control in view
                    srcImage,
                    new Point[] {
                    new Point(0, 0),
                    new Point(targetWidth, 0),
                    new Point(0, targetHeight)
                    },
                    new System.Drawing.Rectangle(0, 0, ToInt32(srcImage.Width), ToInt32(srcImage.Height)),
                    GraphicsUnit.Pixel
                    );
            }
            compressedImage.Save(path);

            var bmp = new BitmapImage(new Uri(new Uri(Environment.CurrentDirectory), @"..\tmp\" + ImageID + ".png"));
            LastCroppedImage.Source = bmp;
        }

        #endregion
    }
}
