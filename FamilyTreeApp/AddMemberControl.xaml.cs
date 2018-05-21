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

namespace FamilyTreeApp
{
    /// <summary>
    /// Interaction logic for AddMemberControl.xaml
    /// </summary>
    public partial class AddMemberControl : UserControl
    {

        public Member Member { get; set; }

        #region Ctor

        public AddMemberControl(object dataContext, Member newMember)
        {
            InitializeComponent();

            Member = newMember;
            DataContext = dataContext;

            CommandBinding UploadImageCommandBinding = new CommandBinding(
                uploadImageButton.Command,
                (dataContext as ViewModel).UploadImageExecuted,
                (dataContext as ViewModel).UploadImageCanExecute
                );
            CommandBinding SaveCommandBinding = new CommandBinding(
                saveButton.Command,
                SaveButtonExecuted,
                SaveButtonCanExecute
                );
            CommandBinding CloseCommandBinding = new CommandBinding(
                closeButton.Command,
                (s, e) => Close(false),
                (s, e) => e.CanExecute = true
                );
            
            this.CommandBindings.Add(UploadImageCommandBinding);
            this.CommandBindings.Add(SaveCommandBinding);
            this.CommandBindings.Add(CloseCommandBinding);
            this.InputBindings.Add(new InputBinding(saveButton.Command, new KeyGesture(Key.Enter)));
            txtbFName.Focus();
        }

        #endregion

        #region Methods

        private void SaveButtonCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //temporarilly!!

            if (
                txtbFName.Text == string.Empty ||
                txtbLName.Text == string.Empty ||
                (maleRadioButton.IsChecked == false && femaleRadioButton.IsChecked == false) ||
                (livingRadioButton.IsChecked == false && deceasedRadioButton.IsChecked == false)
                )
                e.CanExecute = false;
            else
                e.CanExecute = true;
            //e.CanExecute = true;
        }

        private void SaveButtonExecuted(object sender, ExecutedRoutedEventArgs e) =>
            Close(true);
        
        private void Close(bool dialogResult)
        {
            var parent = this.Parent as Window;
            if (parent != null)
                parent.DialogResult = dialogResult;

            parent.Close();
        }

        #endregion

    }
}
