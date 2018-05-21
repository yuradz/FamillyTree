using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
namespace FamilyTreeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            ScaleTransform scaler = WorkSurface.LayoutTransform as ScaleTransform;
            if (scaler == null)
            {
                scaler = new ScaleTransform(1.0, 1.0);
                WorkSurface.LayoutTransform = scaler;
            }
            WorkSurface.MouseWheel += Zoom_MouseWheel;

            //var f = new MemberControl();
            //f.Margin = new Thickness(WorkSurface.Width / 2 - f.Width / 2, WorkSurface.Height / 2 - f.Height / 2, 0, 0);
            //WorkSurface.Children.Add(f);

            //var s = new MemberInfoControl(viewModel);
            //s.Margin = new Thickness(25, 65, 0, 0);
            //WorkSurface.Children.Add(s);

            //var f = new AddMemberControl(viewModel);
            //f.Margin = new Thickness(WorkSurface.Width / 2 - f.Width / 2, WorkSurface.Height / 2 - f.Height / 2, 0, 0);
            //WorkSurface.Children.Add(f);
        }

        #region Event handlers

        private void Zoom_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            var scaler = WorkSurface.LayoutTransform as ScaleTransform;
            if (e.Delta < 0)
            {
                scaler.ScaleX = Math.Max(1, scaler.ScaleX + e.Delta * 0.0005);
                scaler.ScaleY = Math.Max(1, scaler.ScaleY + e.Delta * 0.0005);
            }
            else
            {
                scaler.ScaleX = Math.Min(1.5, scaler.ScaleX + e.Delta * 0.0005);
                scaler.ScaleY = Math.Min(1.5, scaler.ScaleY + e.Delta * 0.0005);
            }
            if (scaler.ScaleX > 1)
            {
                WorkSurfaceScaler.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                WorkSurfaceScaler.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                WorkSurfaceScaler.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                WorkSurfaceScaler.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CreateMember(this);
            //viewModel.LocateMember();
        }
    }
}
