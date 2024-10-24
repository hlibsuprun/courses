using System.Text.RegularExpressions;

namespace FieldValidatorAPI;

public delegate bool RequiredValidDel(string fieldVal);

public delegate bool StringLengthValidDel(string fieldVal, int min, int max);

public delegate bool DateValidDel(string fieldVal, out DateTime validDateTime);

public delegate bool PatternMatchValidDel(string fieldVal, string pattern);

public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare);

public class CommonFieldValidatorFunctions
{
    private static RequiredValidDel _requiredValidDel;
    private static StringLengthValidDel _stringLengthValidDel;
    private static DateValidDel _dateValidDel;
    private static PatternMatchValidDel _patternMatchValidDel;
    private static CompareFieldsValidDel _compareFieldsValidDel;

    public static RequiredValidDel RequiredFieldValidDel
    {
        get
        {
            if (_requiredValidDel == null)
                _requiredValidDel = RequiredFieldValid;

            return _requiredValidDel;
        }
    }

    public static StringLengthValidDel StringLengthFieldValidDel
    {
        get
        {
            if (_stringLengthValidDel == null)
                _stringLengthValidDel = StringFieldLengthValid;

            return _stringLengthValidDel;
        }
    }

    public static DateValidDel DateFieldValidDel
    {
        get
        {
            if (_dateValidDel == null)
                _dateValidDel = DateFieldValid;

            return _dateValidDel;
        }
    }

    public static PatternMatchValidDel PatternMatchValidDel
    {
        get
        {
            if (_patternMatchValidDel == null)
                _patternMatchValidDel = FieldPatternValid;

            return _patternMatchValidDel;
        }
    }

    public static CompareFieldsValidDel FieldsCompareValidDel
    {
        get
        {
            if (_compareFieldsValidDel == null)
                _compareFieldsValidDel = FieldComparisonValid;

            return _compareFieldsValidDel;
        }
    }


    private static bool RequiredFieldValid(string fieldVal)
    {
        if (!string.IsNullOrEmpty(fieldVal))
            return true;

        return false;
    }

    private static bool StringFieldLengthValid(string fieldVal, int min, int max)
    {
        if (fieldVal.Length >= min && fieldVal.Length <= max)
            return true;

        return false;
    }

    private static bool DateFieldValid(string dateTime, out DateTime validDateTime)
    {
        if (DateTime.TryParse(dateTime, out validDateTime))
            return true;

        return false;
    }

    private static bool FieldPatternValid(string fieldVal, string regularExpressionPattern)
    {
        var regex = new Regex(regularExpressionPattern);

        if (regex.IsMatch(fieldVal))
            return true;

        return false;
    }

    private static bool FieldComparisonValid(string field1, string field2)
    {
        if (field1.Equals(field2))
            return true;

        return false;
    }
}