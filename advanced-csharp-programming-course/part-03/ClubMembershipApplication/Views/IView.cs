using ClubMembershipApplication.FieldValidators;

namespace ClubMembershipApplication.Views;

public interface IView
{
    IFieldValidator FieldValidator { get; }
    void RunView();
}