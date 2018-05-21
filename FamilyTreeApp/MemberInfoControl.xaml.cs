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
    /// Interaction logic for MemberInfoControl.xaml
    /// </summary>
    public partial class MemberInfoControl : UserControl
    {

        #region Properties

        public static Member Member { get; set; }
        private static MemberInfoControl This { get; set; }

        public Member MemberWithChanges { get; set; }

        #endregion

        #region Ctor
        
        public MemberInfoControl(ViewModel dataContext, Member member)
        {
            InitializeComponent();

            this.DataContext = dataContext;
            this.MemberWithChanges = member.Clone() as Member;
            MemberInfoControl.Member = member;

            CommandBinding UploadImageCommandBinding = new CommandBinding(
                uploadImageButton.Command,
                dataContext.UploadImageExecuted,
                dataContext.UploadImageCanExecute
                );
            CommandBinding SaveCommandBinding = new CommandBinding(
                saveButton.Command,
                SaveExecuted,
                SaveCanExecute
                );
            CommandBinding CloseCommandBinding = new CommandBinding(
                closeButton.Command,
                (s, e) => Close(),
                (s, e) => e.CanExecute = true
                );

            this.CommandBindings.Add(UploadImageCommandBinding);
            this.CommandBindings.Add(SaveCommandBinding);
            this.CommandBindings.Add(CloseCommandBinding);
            this.InputBindings.Add(new InputBinding(saveButton.Command, new KeyGesture(Key.Escape)));
            This = this;
        }

        #endregion

        #region Methods
        
        public static bool IsOpen =>
            This == null ? false : true;

        public static void Close()
        {
            (Application.Current.MainWindow as MainWindow).WorkSurface.Children.Remove(This);

            Member = null;
            This = null;
        }

        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!MemberWithChanges.HasErrors)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Member.UpdateChanges(MemberWithChanges);
            Close();
        }

        #endregion

        #region Event Handlers
        
        private void cancelButton_Click(object sender, RoutedEventArgs e) =>
            Close();
        
        private void closeButton_Click(object sender, RoutedEventArgs e) =>
            Close();

        #endregion
    }
}
