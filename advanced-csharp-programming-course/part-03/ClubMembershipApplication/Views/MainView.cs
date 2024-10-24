using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views;

internal class MainView : IView
{
    private readonly IView _loginView;

    private readonly IView _registerView;

    public MainView(IView registerView, IView loginView)
    {
        _registerView = registerView;
        _loginView = loginView;
    }

    public IFieldValidator FieldValidator => null;

    public void RunView()
    {
        CommonOutputText.WriteMainHeading();

        Console.WriteLine("Please press 'l' to login or if you are not yet registered please press 'r'");

        var key = Console.ReadKey().Key;

        if (key == ConsoleKey.R)
        {
            RunUserRegistrationView();
            RunLoginView();
        }
        else if (key == ConsoleKey.L)
        {
            RunLoginView();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Goodbye");
            Console.ReadKey();
        }
    }

    private void RunUserRegistrationView()
    {
        _registerView.RunView();
    }

    private void RunLoginView()
    {
        _loginView.RunView();
    }
}