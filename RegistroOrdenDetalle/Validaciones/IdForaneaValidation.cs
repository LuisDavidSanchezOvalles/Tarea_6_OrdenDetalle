using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace RegistroOrdenDetalle.Validaciones
{
    class IdForaneaValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int id = 0;
                try
                {
                    id = Convert.ToInt32(value);
                }
                catch
                {
                    return new ValidationResult(false, "El Id Debe Ser Entero");
                }

                if (id > 0)
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "La Cantidad debe Ser Mayor que 0");

            }
            return new ValidationResult(false, "Debes Colocar Un Id Valido");
        }
    }
}
