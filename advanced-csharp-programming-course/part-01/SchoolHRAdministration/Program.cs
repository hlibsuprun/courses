﻿using HRAdministrationAPI;

namespace SchoolHRAdministration;

public enum EmployeeType
{
    Teacher,
    HeadOfDepartment,
    DeputyHeadMaster,
    HeadMaster
}

internal abstract class Program
{
    private static void Main()
    {
        // decimal totalSalaries = 0;
        var employees = new List<IEmployee>();

        SeedData(employees);

        // foreach (var employee in employees) totalSalaries += employee.Salary;
        //
        // Console.WriteLine($"Total Annual Salaries (Including bonus): {totalSalaries}");

        Console.WriteLine($"Total Annual Salaries (Including bonus): {employees.Sum(emp => emp.Salary)}");
    }

    private static void SeedData(List<IEmployee> employees)
    {
        var teacher1 = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 1, "Bob", "Fisher", 40000);
        employees.Add(teacher1);

        var teacher2 = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 2, "Jenny", "Thomas", 40000);
        employees.Add(teacher2);

        var headOfDepartment =
            EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadOfDepartment, 3, "Brenda", "Mullins", 50000);
        employees.Add(headOfDepartment);

        var deputyHeadMaster =
            EmployeeFactory.GetEmployeeInstance(EmployeeType.DeputyHeadMaster, 4, "Devlin", "Brown", 60000);
        employees.Add(deputyHeadMaster);

        var headMaster = EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadMaster, 5, "Demien", "Jones", 80000);
        employees.Add(headMaster);
    }
}

public class Teacher : EmployeeBase
{
    public override decimal Salary => base.Salary + base.Salary * 0.02m;
}

public class HeadOfDepartment : EmployeeBase
{
    public override decimal Salary => base.Salary + base.Salary * 0.03m;
}

public class DeputyHeadMaster : EmployeeBase
{
    public override decimal Salary => base.Salary + base.Salary * 0.04m;
}

public class HeadMaster : EmployeeBase
{
    public override decimal Salary => base.Salary + base.Salary * 0.05m;
}

public static class EmployeeFactory
{
    public static IEmployee GetEmployeeInstance(EmployeeType employeeType, int id, string firstName, string lastName,
        decimal salary)
    {
        IEmployee employee = null;

        switch (employeeType)
        {
            case EmployeeType.Teacher:
                employee = FactoryPattern<IEmployee, Teacher>.GetInstance();
                break;
            case EmployeeType.HeadOfDepartment:
                employee = FactoryPattern<IEmployee, HeadOfDepartment>.GetInstance();
                break;
            case EmployeeType.DeputyHeadMaster:
                employee = FactoryPattern<IEmployee, DeputyHeadMaster>.GetInstance();
                break;
            case EmployeeType.HeadMaster:
                employee = FactoryPattern<IEmployee, HeadMaster>.GetInstance();
                break;
        }

        if (employee != null)
        {
            employee.Id = id;
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Salary = salary;
        }
        else
        {
            throw new NullReferenceException();
        }

        return employee;
    }
}