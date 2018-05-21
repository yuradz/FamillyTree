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
    public partial class Member : INotifyPropertyChanged, ICloneable
    {

        #region Properties

        private string _firstName;
        [Required]
        public string FirstName {
            get => _firstName;
            set {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        [Required]
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

        public Image _photo = new Image();

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

        public object Clone()
        {
            var clone = new Member();
            clone.IsDeceasedChecked = this.IsDeceasedChecked;
            clone.BirthDate = this.BirthDate;
            clone.BirthPlace = this.BirthPlace;
            clone.DeathDate = this.DeathDate;
            clone.DeathPlace = this.DeathPlace;
            clone.FirstName = this.FirstName;
            clone.IsFemaleChecked = this.IsFemaleChecked;
            clone.IsLivingChecked = this.IsLivingChecked;
            clone.IsMaleChecked = this.IsMaleChecked;
            clone.LastName = this.LastName;
            clone.PhotoSource = this.PhotoSource?.Clone();
            return clone;
        }

        public void UpdateChanges(Member updatedMember)
        {
            this.IsDeceasedChecked = updatedMember.IsDeceasedChecked;
            this.FirstName = updatedMember.FirstName;
            this.LastName = updatedMember.LastName;
            this.BirthDate = updatedMember.BirthDate;
            this.DeathDate = updatedMember.DeathDate;
            this.BirthPlace = updatedMember.BirthPlace;
            this.DeathPlace = updatedMember.DeathPlace;
            this.IsFemaleChecked = updatedMember.IsFemaleChecked;
            this.IsLivingChecked = updatedMember.IsLivingChecked;
            this.IsMaleChecked = updatedMember.IsMaleChecked;
            this.PhotoSource = updatedMember.PhotoSource;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
