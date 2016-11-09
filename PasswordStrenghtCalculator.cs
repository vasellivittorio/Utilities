using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPassword.Utilities
{
    class PasswordStrenghtCalculator
    {
        public static int Get(string password)
        {
            int valutazione = 0;
            int maiuscole = 0;
            int minuscole = 0;
            int numeri = 0;
            int simboli = 0;
            foreach(char c in password)
            {
                if (char.IsDigit(c))
                    numeri++;
                if (char.IsPunctuation(c))
                    simboli++;
                if (char.IsLower(c))
                    minuscole++;
                if (char.IsUpper(c))
                    maiuscole++;
            }
            if (maiuscole > 0)
                valutazione++;
            if (minuscole > 0)
                valutazione++;
            if (numeri > 0)
                valutazione++;
            if (simboli > 0)
                valutazione++;
            if (simboli + maiuscole + minuscole + numeri > 7)
                valutazione += 2;
            if (valutazione > 5)
                valutazione = 5;

            return valutazione;
        }
    }
    
}
