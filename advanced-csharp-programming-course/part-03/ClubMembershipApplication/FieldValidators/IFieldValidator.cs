namespace ClubMembershipApplication.FieldValidators;

public delegate bool FieldValidatorDel(int fieldIndex, string fieldValue, string[] fieldArray,
    out string fieldInvalidMessage);

public interface IFieldValidator
{
    string[] FieldArray { get; }
    FieldValidatorDel ValidatorDel { get; }
    void InitialiseValidatorDelegates();
}