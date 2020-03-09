using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace RegistroOrdenDetalle.Validaciones
{
    public class TelefonoValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Debes poner algun Telefono");
        }
    }
}
