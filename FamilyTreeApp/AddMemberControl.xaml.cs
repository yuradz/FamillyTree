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
            this.CommandBindings.Add(UploadImageCommandBinding);
            this.CommandBindings.Add(SaveCommandBinding);
        }
        
        public Member Member { get; set; }

        private void SaveButtonCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (
                txtbFName.Text == string.Empty ||
                txtbLName.Text == string.Empty ||
                (maleRadioButton.IsChecked == false && femaleRadioButton.IsChecked == false) ||
                (livingRadioButton.IsChecked == false && deceasedRadioButton.IsChecked == false)
                )
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void SaveButtonExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Member.HasErrors)
                return;
            
            //Member.BirthDate = birthDatePicker.Text == String.Empty ? DateTime.MinValue : Convert.ToDateTime(birthDatePicker.Text);
            //Member.DeathDate = (deathDatePicker == null || deathDatePicker.Text == String.Empty) ? DateTime.MinValue : Convert.ToDateTime(deathDatePicker.Text);
            var parent = this.Parent as Window;
            if (parent != null)
                parent.DialogResult = true;

            parent.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("!");
        }
    }
}
