using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views;

public class UserRegistrationView : IView
{
    private readonly IRegister _register;

    public UserRegistrationView(IRegister register, IFieldValidator fieldValidator)
    {
        FieldValidator = fieldValidator;
        _register = register;
    }

    public IFieldValidator FieldValidator { get; }

    public void RunView()
    {
        CommonOutputText.WriteMainHeading();
        CommonOutputText.WriteRegistrationHeading();

        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.EmailAddress] =
            GetInputFromUser(FieldConstants.UserRegistrationField.EmailAddress, "Please enter your email address: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.FirstName] =
            GetInputFromUser(FieldConstants.UserRegistrationField.FirstName, "Please enter your first name: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.LastName] =
            GetInputFromUser(FieldConstants.UserRegistrationField.LastName, "Please enter your last name: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.Password] = GetInputFromUser(
            FieldConstants.UserRegistrationField.Password,
            $"Please enter your password.{Environment.NewLine}(Your password must contain at least 1 small-case letter,{Environment.NewLine}1 Capital letter, 1 digit, 1 special character{Environment.NewLine} and the length should be between 6-10 characters): ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PasswordCompare] =
            GetInputFromUser(FieldConstants.UserRegistrationField.PasswordCompare, "Please re-enter your password: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.DateOfBirth] =
            GetInputFromUser(FieldConstants.UserRegistrationField.DateOfBirth, "Please enter your date of birth: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PhoneNumber] =
            GetInputFromUser(FieldConstants.UserRegistrationField.PhoneNumber, "Please enter your phone number: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressFirstLine] = GetInputFromUser(
            FieldConstants.UserRegistrationField.AddressFirstLine, "Please enter the first line of your address: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressSecondLine] = GetInputFromUser(
            FieldConstants.UserRegistrationField.AddressSecondLine, "Please enter the second line of your address: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressCity] =
            GetInputFromUser(FieldConstants.UserRegistrationField.AddressCity,
                "Please enter the city where you live: ");
        FieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PostCode] =
            GetInputFromUser(FieldConstants.UserRegistrationField.PostCode, "Please enter your post code: ");

        RegisterUser();
    }

    private void RegisterUser()
    {
        _register.Register(FieldValidator.FieldArray);

        CommonOutputFormat.ChangeFontColor(FontTheme.Success);
        Console.WriteLine("You have successfully registered. Please press any key to login");
        CommonOutputFormat.ChangeFontColor(FontTheme.Default);
        Console.ReadKey();
    }

    private string GetInputFromUser(FieldConstants.UserRegistrationField field, string promptText)
    {
        var fieldVal = "";

        do
        {
            Console.Write(promptText);
            fieldVal = Console.ReadLine();
        } while (!FieldValid(field, fieldVal));

        return fieldVal;
    }

    private bool FieldValid(FieldConstants.UserRegistrationField field, string fieldValue)
    {
        if (!FieldValidator.ValidatorDel((int)field, fieldValue, FieldValidator.FieldArray, out var invalidMessage))
        {
            CommonOutputFormat.ChangeFontColor(FontTheme.Danger);

            Console.WriteLine(invalidMessage);

            CommonOutputFormat.ChangeFontColor(FontTheme.Default);

            return false;
        }

        return true;
    }
}