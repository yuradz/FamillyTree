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

        //private static int IDcounter = 1;

        //private int ID { get; }

        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register("FirstName", typeof(string), typeof(MemberInfoControl), new PropertyMetadata());



        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register("LastName", typeof(string), typeof(MemberInfoControl), new PropertyMetadata());


        
        public DateTime BirthDate
        {
            get { return (DateTime)GetValue(BirthDateProperty); }
            set { SetValue(BirthDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BirthDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BirthDateProperty =
            DependencyProperty.Register("BirthDate", typeof(DateTime), typeof(MemberInfoControl), new PropertyMetadata());



        public DateTime DeathDate
        {
            get { return (DateTime)GetValue(DeathDateProperty); }
            set { SetValue(DeathDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeathDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeathDateProperty =
            DependencyProperty.Register("DeathDate", typeof(DateTime), typeof(MemberInfoControl), new PropertyMetadata());



        public bool? IsDeceasedActivated
        {
            get { return (bool?)GetValue(IsDeceasedActivatedProperty); }
            set { SetValue(IsDeceasedActivatedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDeceasedActivated.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDeceasedActivatedProperty =
            DependencyProperty.Register("IsDeceasedActivated", typeof(bool?), typeof(MemberInfoControl), new PropertyMetadata(false));



        #endregion

        public MemberInfoControl()
        {
            InitializeComponent();
        }

        //public MemberInfoControl(object caller)
        //{
        //    InitializeComponent();

        //    if (caller is MainWindow) Caller = caller;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("!");
        }
    }
}
