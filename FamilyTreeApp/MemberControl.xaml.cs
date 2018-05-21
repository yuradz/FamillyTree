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

        public Member Member { get; set; }

        #endregion

        #region Ctors
        
        public MemberControl(ViewModel dataContext, Member member)
        {
            InitializeComponent();

            DataContext = dataContext;
            Member = member;

            CommandBinding UploadImageCommandBinding = new CommandBinding(
                uploadImageButton.Command,
                dataContext.UploadImageExecuted,
                dataContext.UploadImageCanExecute
                );
            
            this.CommandBindings.Add(UploadImageCommandBinding);
        }

        #endregion



        #region Event handlers

        private void memberControl_MouseDoubleClick(object sender, MouseButtonEventArgs e) =>
            (DataContext as ViewModel).ShowDetailedInfo(DataContext, Member);

        #endregion
    }
}
