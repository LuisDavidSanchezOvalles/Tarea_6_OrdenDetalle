using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace RegistroOrdenDetalle.Validaciones
{
    public class CedulaValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string cadena = value as string;
            cadena = cadena.Replace("-", "");

            if (cadena != null)
            {
                if (cadena.Length <= 0)
                    return new ValidationResult(false, "Debes poner una Cedula");

                foreach (var caracter in cadena)
                {
                    if (!char.IsDigit(caracter))
                        return new ValidationResult(false, "La Cedula solo puede tener numeros");
                }

                return ValidationResult.ValidResult;

            }
            return new ValidationResult(false, "Debes poner una Cedula");
        }
    }
}
