using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
    }
}
