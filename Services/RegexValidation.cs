using System.Text.RegularExpressions;

namespace Services
{
    public static class RegexValidation
    {
        //Metodo para el password
        public static bool IsValidPassword(string password)
        {
            // Regla de contraseña: mínimo 8 caracteres, al menos una letra y un número
            string passwordPattern = @"^(?=.*[A-Z])(?=.*\d).{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }

        public static bool IsValidMail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool IsValidFechaVto(string fechaVto)
        {
            return Regex.IsMatch(fechaVto, @"^(0[1-9]|1[0-2])/\d{2}$");
        }

        public static bool IsValidCardNumber(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            string pattern = @"^(?:4[0-9]{12}(?:[0-9]{3})?|   # Visa
                            5[1-5][0-9]{14}|              # MasterCard
                            3[47][0-9]{13})$";

            return Regex.IsMatch(cardNumber, pattern, RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsValidName(string Name)
        {
            // Regex para validar el nombre del titular de la tarjeta
            string pattern = @"^[A-Za-zÀ-ÖØ-öø-ÿ\s'-]+$";

            // Usar Regex para validar
            return Regex.IsMatch(Name, pattern);
        }

    }
}
