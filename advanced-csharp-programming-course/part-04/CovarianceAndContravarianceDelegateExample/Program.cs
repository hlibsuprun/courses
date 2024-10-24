namespace CovarianceAndContravarianceDelegateExample;

internal class Program
{
    private static void Main(string[] args)
    {
        CarFactoryDel carFactoryDel = CarFactory.ReturnICECar;

        var iceCar = carFactoryDel(1, "Audi R8");

        carFactoryDel = CarFactory.ReturnEvCar;

        var evCar = carFactoryDel(2, "Tesla Model-3");

        LogICECarDetailsDel logICECarDetailsDel = LogCarDetails;

        logICECarDetailsDel(iceCar as ICECar);

        LogEVCarDetailsDel logEVCarDetailsDel = LogCarDetails;

        logEVCarDetailsDel(evCar as EVCar);
    }

    private static void LogCarDetails(Car car)
    {
        if (car is ICECar)
        {
            using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICEDetails.txt"),
                       true))
            {
                sw.WriteLine($"Object Type: {car.GetType()}");
                sw.WriteLine($"Car Detials: {car.GetCarDetails()}");
            }

            ;
        }
        else if (car is EVCar)
        {
            Console.WriteLine($"Object Type: {car.GetType()}");
            Console.WriteLine($"Car Detials: {car.GetCarDetails()}");
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private delegate Car CarFactoryDel(int id, string name);

    private delegate void LogICECarDetailsDel(ICECar car);

    private delegate void LogEVCarDetailsDel(EVCar car);
}

public static class CarFactory
{
    public static ICECar ReturnICECar(int id, string name)
    {
        return new ICECar { Id = id, Name = name };
    }

    public static EVCar ReturnEvCar(int id, string name)
    {
        return new EVCar { Id = id, Name = name };
    }
}

public abstract class Car
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual string GetCarDetails()
    {
        return $"{Id} - {Name} ";
    }
}

public class ICECar : Car
{
    public override string GetCarDetails()
    {
        return $"{base.GetCarDetails()} - Internal Combustion Engine";
    }
}

public class EVCar : Car
{
    public override string GetCarDetails()
    {
        return $"{base.GetCarDetails()} - Electric";
    }
}