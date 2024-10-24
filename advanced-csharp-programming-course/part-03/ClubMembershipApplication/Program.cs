using ClubMembershipApplication.Views;

namespace ClubMembershipApplication;

internal class Program
{
    private static void Main(string[] args)
    {
        IView mainView = Factory.GetMainViewObject();
        mainView.RunView();

        Console.ReadKey();
    }
}