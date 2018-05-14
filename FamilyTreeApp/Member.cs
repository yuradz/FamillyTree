using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;

namespace FamilyTreeApp
{
    public partial class Member : INotifyPropertyChanged
    {

        #region Properties

        private string _firstName;
        [StringLength(15)]
        public string FirstName {
            get => _firstName;
            set {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        [StringLength(15)]
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _birthDate;
        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _deathDate = null;
        public DateTime? DeathDate
        {
            get => _deathDate;
            set
            {
                _deathDate = value;
                OnPropertyChanged();
            }
        }

        private string _birthPlace;
        public string BirthPlace
        {
            get => _birthPlace;
            set
            {
                _birthPlace = value;
                OnPropertyChanged();
            }
        }

        private string _deathPlace;
        public string DeathPlace
        {
            get => _deathPlace;
            set
            {
                _deathPlace = value;
                OnPropertyChanged();
            }
        }

        private bool? _isDeceasedChecked = false;
        public bool? IsDeceasedChecked
        {
            get => _isDeceasedChecked;
            set
            {
                _isDeceasedChecked = value;
                if (value == true)
                    DeathDate = null;

                OnPropertyChanged();
            }
        }

        private bool? _isLivingChecked = false;
        public bool? IsLivingChecked
        {
            get => _isLivingChecked;
            set
            {
                _isLivingChecked = value;
                if (value == true)
                {
                    DeathPlace = null;
                    DeathDate = DateTime.MinValue;
                }
                OnPropertyChanged();
            }
        }

        private bool? _isMaleChecked = false;
        public bool? IsMaleChecked
        {
            get => _isMaleChecked;
            set
            {
                _isMaleChecked = value;
                OnPropertyChanged();
            }
        }

        private bool? _isFemaleChecked = false;
        public bool? IsFemaleChecked
        {
            get => _isFemaleChecked;
            set
            {
                _isFemaleChecked = value;
                OnPropertyChanged();
            }
        }

        private Image _photo = new Image();
        public System.Windows.Media.ImageSource PhotoSource
        {
            get => _photo.Source;
            set
            {
                _photo.Source = value;
                OnPropertyChanged();
            }
        }

        #endregion

        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
