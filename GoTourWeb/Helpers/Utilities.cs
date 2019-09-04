using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GoTourWeb.Models;

namespace GoTourWeb.Helpers {
    public static class Utilities {

        public static string  GetWidthForMenuCities(int valCount) {
            var vResult = "50%";
            if (valCount > 0 && valCount <= 20) {
                return vResult = "16%";
            } else if (valCount > 20 && valCount <= 40) {
                return vResult = "32%";
            }
                return vResult;
        }
        public static string GetColForMenuCities(int valCount) {
            var vResult = "4";
            if (valCount > 0 && valCount <= 20) {
                return vResult = "12";
            } else if (valCount > 20 && valCount <= 40) {
                return vResult = "6";
            }
            return vResult;
        }

        public static string GetPaymentMethod(string vCardNumber) {
            string vResult = "VISA";
            Regex vRegexVisa = new Regex(@"^4[0-9]{6,}$");
            Regex vRegexMaster = new Regex(@"^5[1-5][0-9]{5,}|222[1-9][0-9]{3,}|22[3-9][0-9]{4,}|2[3-6][0-9]{5,}|27[01][0-9]{4,}|2720[0-9]{3,}$");
            Regex vRegexAmerican = new Regex(@"^3[47][0-9]{5,}$");
            Regex vRegexDinners = new Regex(@"^3(?:0[0-5]|[68][0-9])[0-9]{4,}$");
            Match vMatch = vRegexVisa.Match(vCardNumber);
            if (vMatch.Success) {
                vResult= "VISA";
                return vResult;
            }
            vMatch = vRegexDinners.Match(vCardNumber);
            if (vMatch.Success) {
                vResult = "DINNERS";
                return vResult;
            }
            vMatch = vRegexMaster.Match(vCardNumber);
            if (vMatch.Success) {
                vResult = "MASTER";
                return vResult;
            }
            vMatch = vRegexAmerican.Match(vCardNumber);
            if (vMatch.Success) {
                vResult = "AMEX";
                return vResult;
            }
            
            return vResult;
        }
    }
}
