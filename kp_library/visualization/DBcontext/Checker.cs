using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Db_lib;

public static class Checker
{
    public static bool is_empty(string line)
    {
        if (line == "") return true; else return false;
    }

    public static bool phoneNumber_is_valid(string phoneNumber)
    {
        string pattern = @"^(\+7|8)\d{10}$";
        if (Regex.IsMatch(phoneNumber, pattern)) return true; else return false;
    }

    public static string phoneNumber_to_format(string phoneNumber)
    {
        string formattedNumber;
        if (phoneNumber.StartsWith("+7"))
            formattedNumber = $"+7 ({phoneNumber.Substring(2, 3)}) {phoneNumber.Substring(5, 3)}-{phoneNumber.Substring(8, 2)}-{phoneNumber.Substring(10, 2)}";
        else
            formattedNumber = $"+7 ({phoneNumber.Substring(1, 3)}) {phoneNumber.Substring(4, 3)}-{phoneNumber.Substring(7, 2)}-{phoneNumber.Substring(9, 2)}";
        return formattedNumber;
    }

    public static bool textField_is_valid(string? field)
    {
        foreach (char letter in field)
            if (!char.IsLetter(letter) && letter != ' ' && letter != '-') return false;
        return true;
    }

    public static bool numField_is_valid(string field)
    {
        foreach (char letter in field)
            if (!char.IsDigit(letter)) return false;
        return true;
    }
}
