using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace RegistroOrdenDetalle.Validaciones
{
    public class PrecioValidacion : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                decimal precio = 0;
                try
                {
                    precio = Convert.ToDecimal(value);
                }
                catch
                {
                    return new ValidationResult(false, "El Precio Debe Ser un Número Decimal o Entero");
                }

                if (precio > 0)
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "El Precio debe Ser Mayor que 0");

            }
            return new ValidationResult(false, "Debes Colocar un Precio");
        }
    }
}
