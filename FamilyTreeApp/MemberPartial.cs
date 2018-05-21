using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTreeApp
{
    public partial class Member: IDataErrorInfo
    {
        private List<string> PropertiesWithErrors { get; set; } = new List<string>();
        public bool HasErrors => PropertiesWithErrors.Count != 0;

        public string this[string propertyName]
        {
            get
            {
                string[] errors = null;
                switch (propertyName)
                {
                    case nameof(FirstName):
                        errors = GetErrorsFromAnnotations(nameof(FirstName), FirstName);
                        break;
                    case nameof(LastName):
                        errors = GetErrorsFromAnnotations(nameof(LastName), LastName);
                        break;
                }
                if (errors != null && errors.Length != 0)
                {
                    PropertiesWithErrors.Add(propertyName);
                    return "error";
                }
                else
                    PropertiesWithErrors.Remove(propertyName);

                return string.Empty;
            }
        }

        public string Error => throw new NotImplementedException();

        private string[] GetErrorsFromAnnotations<T>(string propertyName, T value)
        {
            var results = new List<ValidationResult>();
            var vc = new ValidationContext(this, null, null) { MemberName = propertyName };
            var isValid = Validator.TryValidateProperty(value, vc, results);
            return (isValid) ? null : Array.ConvertAll(results.ToArray(), o => o.ErrorMessage);
        }
    }
}
