using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views;

public class UserLoginView : IView
{
    private readonly ILogin _loginUser;

    public UserLoginView(ILogin login)
    {
        _loginUser = login;
    }

    public IFieldValidator FieldValidator => null;

    public void RunView()
    {
        CommonOutputText.WriteMainHeading();

        CommonOutputText.WriteLoginHeading();

        Console.WriteLine("Please enter your email address");

        var emailAddress = Console.ReadLine();

        Console.WriteLine("Please enter your password");

        var password = Console.ReadLine();

        var user = _loginUser.Login(emailAddress, password);

        if (user != null)
        {
            WelcomeUserView welcomeUserView = new WelcomeUserView(user);
            welcomeUserView.RunView();
        }
        else
        {
            Console.Clear();
            CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
            Console.WriteLine("The credentials that you entered do not match any of our records");
            CommonOutputFormat.ChangeFontColor(FontTheme.Default);
            Console.ReadKey();
        }
    }
}