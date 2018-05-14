using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.Win32;

namespace FamilyTreeApp
{
    public class ViewModel : INotifyPropertyChanged
    {
        int x = 1216;
        int y = 10;
        private ObservableCollection<Member> MembersCollection = new ObservableCollection<Member>();

        public void CreateMember(Window parentWindow)
        {
            var newMember = new Member();
            var addMemberControl = new AddMemberControl(this, newMember);

            Window addMemberWindow = new Window
            {
                Title = "My User Control Dialog",
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Background = new SolidColorBrush(Colors.Transparent),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = addMemberControl
            };
            addMemberWindow.Owner = parentWindow;
            addMemberWindow.ShowInTaskbar = false;
            var result = addMemberWindow.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                MessageBox.Show("OK");
                //AddMemberToCollection(newMember);
                var mainWindow = Application.Current.MainWindow as MainWindow;

                var f = new MemberControl(newMember);
                f.Margin = new Thickness(x, y, 0, 0);
                mainWindow.WorkSurface.Children.Add(f);
                x -= 200;
            }
        }
        
        public static RoutedCommand UploadImage = new RoutedCommand();

        public void UploadImageCanExecute(object sender, CanExecuteRoutedEventArgs e) 
            => e.CanExecute = true;

        public void UploadImageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                var cropWindowdialog = new CropWindow(ofd.FileName, e.Parameter);
                var result = cropWindowdialog.ShowDialog();
                if (result.HasValue && result.Value == true)
                {
                    MessageBox.Show("OK");
                    Image targetFoto = null;

                    if (e.Parameter.GetType() == typeof(MemberControl)) targetFoto = (e.Parameter as MemberControl).memberFacePic;
                    else if (e.Parameter.GetType() == typeof(MemberInfoControl)) targetFoto = (e.Parameter as MemberInfoControl).memberFacePic;
                    else if (e.Parameter.GetType() == typeof(AddMemberControl)) targetFoto = (e.Parameter as AddMemberControl).memberFacePic;

                    targetFoto.Source = CropWindow.LastCroppedImage.Source;
                }
            }
        }

        //public void AddMemberToCollection(MemberControl newMember) => MembersCollection.Add(newMember);
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
