namespace GenericsBasics;

internal class Program
{
    private static void Main(string[] args)
    {
        var salaries = new Salaries();

        // ArrayList salaryList = salaries.GetSalaries();
        var salaryList = salaries.GetSalaries();

        var salary = salaryList[1];

        salary = salary + salary * 0.02f;

        Console.WriteLine(salary);

        Console.ReadKey();
    }
}

public class Salaries
{
    //ArrayList _salaryList = new ArrayList();
    private readonly List<float> _salaryList = new();

    public Salaries()
    {
        /*_salaryList.Add(60000.34);
        _salaryList.Add(40000.51);
        _salaryList.Add(20000.23);*/

        _salaryList.Add(60000.34f);
        _salaryList.Add(40000.51f);
        _salaryList.Add(20000.23f);
    }

    // public ArrayList GetSalaries()
    public List<float> GetSalaries()
    {
        return _salaryList;
    }
}