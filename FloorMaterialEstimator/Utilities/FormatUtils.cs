using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class FormatUtils
    {
        public static string FormatSqrInchesToSqrFeetAndInches(double sqrInches, bool roundDown = true)
        {
            int intSqrInches = 0;

            bool isNegative = sqrInches < 0.0;

            if (isNegative)
            {
                sqrInches = -sqrInches;
            }

            if (roundDown)
            {
                intSqrInches = (int)Math.Floor(sqrInches);
            }

            else
            {
                intSqrInches = (int)Math.Round(sqrInches);
            }

            int feet = intSqrInches / 144;
            int inch = intSqrInches % 144;

            string rtnValue = string.Empty;

            if (feet == 0)
            {
                rtnValue = inch.ToString() + '"';
            }

            else
            {
                rtnValue = feet.ToString("#,##0") + "' " + inch.ToString() + '"';
            }

            if (isNegative)
            {
                return '-' + rtnValue;
            }

            else
            {
                return rtnValue;
            }
        }


        public static string FormatInchesToFeetAndInches(double inches, int precision)
        {
            int intInches = 0;

            bool isNegative = inches < 0.0;

            if (isNegative)
            {
                inches = -inches;
            }

            int intInch = (int)Math.Floor(inches);
            int intFeet = intInch / 12;

            double residInch = inches - (12 * intFeet);

            string rtnValue = intFeet.ToString() + "' ";

            if (precision <= 0)
            {
                rtnValue += intInch.ToString() + '"';
            }

            else if (precision == 1)
            {
                rtnValue += residInch.ToString("0.0") + '"';
            }

            else if (precision == 2)
            {
                rtnValue += residInch.ToString("0.00") + '"';
            }

            else
            {
                string fmt = "0." + new string('0', precision);
                rtnValue += residInch.ToString(fmt) + '"';
            }

            return rtnValue;
        }


        public static string FormatInchesToFeetAndInches(double inches, bool roundDown = true)
        {
            int intInches = 0;

            bool isNegative = inches < 0.0;

            if (isNegative)
            {
                inches = -inches;
            }

            if (roundDown)
            {
                intInches = (int) Math.Floor(inches);
            }

            else
            {
                intInches = (int)Math.Round(inches);
            }

            int feet = intInches / 12;
            int inch = intInches % 12;

            string rtnValue = string.Empty;
            
            if (feet == 0)
            {
                rtnValue = inch.ToString() + '"';
            }

            else
            {
                rtnValue = feet.ToString("#,##0") + "' " + inch.ToString() + '"';
            }
            
            if (isNegative)
            {
                return '-' + rtnValue;
            }

            else
            {
                return rtnValue;
            }
        }

        public static string FormatFeetToFeetAndInches(double feet, bool roundDown = true)
        {
            double inches = feet * 12.0;

            return FormatInchesToFeetAndInches(inches, roundDown);
            
        }

        public static string FormatGuidAbbreviation(string guid)
        {
            if (String.IsNullOrEmpty(guid))
            {
                return "";
            }

            if (guid.Length <= 8)
            {
                return guid;
            }

            return guid.Substring(0, 8);
        }

        public static string CenterText(string text, int fieldWidth)
        {
            if (fieldWidth <= 0)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', fieldWidth);
            }

            int delta = fieldWidth - text.Length;

            if (delta <= 0)
            {
                return text;
            }

            int delta1 = delta / 2;
            int delta2 = delta - delta1;
            
            if (delta1<= 0)
            {
                return text + new string(' ', delta2);
            }

            return new string(' ', delta1) + text + new string(' ', delta2);
        }
    }
}
