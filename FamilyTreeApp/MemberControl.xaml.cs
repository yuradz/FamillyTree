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
using Microsoft.Win32;

namespace FamilyTreeApp
{
    /// <summary>
    /// Interaction logic for MemberInfoControl.xaml
    /// </summary>
    public partial class MemberControl : UserControl
    {
        #region Properties
        
        Member Member { get; set; }

        #endregion

        #region Ctors

        public MemberControl(Member member)
        {
            InitializeComponent();

            DataContext = member;
            Member = member;
        }

        #endregion

        #region Event handlers



        #endregion

        private void UploadImageCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //if (memberFacePic?.Source == null)
            //    e.CanExecute = true;
            //else
            //    e.CanExecute = false;
        }

        private void UploadImageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //var ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == true)
            //{
            //    var cropWindowdialog = new CropWindow(ofd.FileName, this, ID);
            //    var result = cropWindowdialog.ShowDialog();
            //    if (result.HasValue && result.Value == true)
            //    {
            //        MessageBox.Show("OK");
            //        memberFacePic.Source = CropWindow.LastCroppedImage.Source;
            //    }
            //}
        }
    }
}
