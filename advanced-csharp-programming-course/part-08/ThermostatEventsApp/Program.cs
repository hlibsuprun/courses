using System.ComponentModel;

namespace ThermostatEventsApp;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Press any key to start device...");
        Console.ReadKey();

        IDevice device = new Device();

        device.RunDevice();

        Console.ReadKey();
    }
}

public class Device : IDevice
{
    private const double Warning_Level = 27;
    private const double Emergency_Level = 75;

    public double WarningTemperatureLevel => Warning_Level;

    public double EmergencyTemperatureLevel => Emergency_Level;

    public void HandleEmergency()
    {
        Console.WriteLine();
        Console.WriteLine("Sending out notifications to emergency services personal...");
        ShutDownDevice();
        Console.WriteLine();
    }

    public void RunDevice()
    {
        Console.WriteLine("Device is running...");

        ICoolingMechanism coolingMechanism = new CoolingMechanism();
        IHeatSensor heatSensor = new HeatSensor(Warning_Level, Emergency_Level);
        IThermostat thermostat = new Thermostat(this, heatSensor, coolingMechanism);

        thermostat.RunThermostat();
    }

    private void ShutDownDevice()
    {
        Console.WriteLine("Shutting down device...");
    }
}

public class Thermostat : IThermostat
{
    private readonly ICoolingMechanism _coolingMechanism;
    private readonly IDevice _device;
    private readonly IHeatSensor _heatSensor;

    public Thermostat(IDevice device, IHeatSensor heatSensor, ICoolingMechanism coolingMechanism)
    {
        _device = device;
        _coolingMechanism = coolingMechanism;
        _heatSensor = heatSensor;
    }

    public void RunThermostat()
    {
        Console.WriteLine("Thermostat is running...");
        WireUpEventsToEventHandlers();
        _heatSensor.RunHeatSensor();
    }

    private void WireUpEventsToEventHandlers()
    {
        _heatSensor.TemperatureReachesWarningLevelEventHandler += HeatSensor_TemperatureReachesWarningLevelEventHandler;
        _heatSensor.TemperatureFallsBelowWarningLevelEventHandler +=
            HeatSensor_TemperatureFallsBelowWarningLevelEventHandler;
        _heatSensor.TemperatureReachesEmergencyLevelEventHandler +=
            HeatSensor_TemperatureReachesEmergencyLevelEventHandler;
    }

    private void HeatSensor_TemperatureReachesEmergencyLevelEventHandler(object sender, TemperatureEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine($"Emergency Alert!! (Emergency level is {_device.EmergencyTemperatureLevel} and above)");
        _device.HandleEmergency();

        Console.ResetColor();
    }

    private void HeatSensor_TemperatureFallsBelowWarningLevelEventHandler(object sender, TemperatureEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
        Console.WriteLine(
            $"Information Alert!! Temperature falls below warning level (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})");
        _coolingMechanism.Off();
        Console.ResetColor();
    }

    private void HeatSensor_TemperatureReachesWarningLevelEventHandler(object sender, TemperatureEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine();
        Console.WriteLine(
            $"Warning Alert!! (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})");
        _coolingMechanism.On();
        Console.ResetColor();
    }
}

public interface IThermostat
{
    void RunThermostat();
}

public interface IDevice
{
    double WarningTemperatureLevel { get; }
    double EmergencyTemperatureLevel { get; }
    void RunDevice();
    void HandleEmergency();
}

public class CoolingMechanism : ICoolingMechanism
{
    public void Off()
    {
        Console.WriteLine();
        Console.WriteLine("Switching cooling mechanism off...");
        Console.WriteLine();
    }

    public void On()
    {
        Console.WriteLine();
        Console.WriteLine("Cooling mechanism is on...");
        Console.WriteLine();
    }
}

public interface ICoolingMechanism
{
    void On();
    void Off();
}

public class HeatSensor : IHeatSensor
{
    private static readonly object _temperatureReachesWarningLevelKey = new();
    private static readonly object _temperatureFallsBelowWarningLevelKey = new();
    private static readonly object _temperatureReachesEmergencyLevelKey = new();
    private readonly double _emergencyLevel;

    private bool _hasReachedWarningTemperature;

    protected EventHandlerList _listEventDelegates = new();

    private double[] _temperatureData;
    private readonly double _warningLevel;

    public HeatSensor(double warningLevel, double emergencyLevel)
    {
        _warningLevel = warningLevel;
        _emergencyLevel = emergencyLevel;

        SeedData();
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesEmergencyLevelEventHandler
    {
        add => _listEventDelegates.AddHandler(_temperatureReachesEmergencyLevelKey, value);

        remove => _listEventDelegates.RemoveHandler(_temperatureReachesEmergencyLevelKey, value);
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureReachesWarningLevelEventHandler
    {
        add => _listEventDelegates.AddHandler(_temperatureReachesWarningLevelKey, value);

        remove => _listEventDelegates.RemoveHandler(_temperatureReachesWarningLevelKey, value);
    }

    event EventHandler<TemperatureEventArgs> IHeatSensor.TemperatureFallsBelowWarningLevelEventHandler
    {
        add => _listEventDelegates.AddHandler(_temperatureFallsBelowWarningLevelKey, value);

        remove => _listEventDelegates.RemoveHandler(_temperatureFallsBelowWarningLevelKey, value);
    }

    public void RunHeatSensor()
    {
        Console.WriteLine("Heat sensor is running...");
        MonitorTemperature();
    }

    private void MonitorTemperature()
    {
        foreach (var temperature in _temperatureData)
        {
            Console.ResetColor();
            Console.WriteLine($"DateTime: {DateTime.Now}, Temperature: {temperature}");

            if (temperature >= _emergencyLevel)
            {
                var e = new TemperatureEventArgs
                {
                    Temperature = temperature,
                    CurrentDateTime = DateTime.Now
                };

                OnTemperatureReachesEmergencyLevel(e);
            }
            else if (temperature >= _warningLevel)
            {
                _hasReachedWarningTemperature = true;

                var e = new TemperatureEventArgs
                {
                    Temperature = temperature,
                    CurrentDateTime = DateTime.Now
                };

                OnTemperatureReachesWarningLevel(e);
            }
            else if (temperature < _warningLevel && _hasReachedWarningTemperature)
            {
                _hasReachedWarningTemperature = false;

                var e = new TemperatureEventArgs
                {
                    Temperature = temperature,
                    CurrentDateTime = DateTime.Now
                };

                OnTemperatureFallsBelowWarningLevel(e);
            }

            Thread.Sleep(1000);
        }
    }

    private void SeedData()
    {
        _temperatureData = new[] { 16, 17, 16.5, 18, 19, 22, 24, 26.75, 28.7, 27.6, 26, 24, 22, 45, 68, 86.45 };
    }

    protected void OnTemperatureReachesWarningLevel(TemperatureEventArgs e)
    {
        var handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesWarningLevelKey];

        if (handler != null) handler(this, e);
    }

    protected void OnTemperatureFallsBelowWarningLevel(TemperatureEventArgs e)
    {
        var handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureFallsBelowWarningLevelKey];

        if (handler != null) handler(this, e);
    }

    protected void OnTemperatureReachesEmergencyLevel(TemperatureEventArgs e)
    {
        var handler = (EventHandler<TemperatureEventArgs>)_listEventDelegates[_temperatureReachesEmergencyLevelKey];

        if (handler != null) handler(this, e);
    }
}

public interface IHeatSensor
{
    event EventHandler<TemperatureEventArgs> TemperatureReachesEmergencyLevelEventHandler;
    event EventHandler<TemperatureEventArgs> TemperatureReachesWarningLevelEventHandler;
    event EventHandler<TemperatureEventArgs> TemperatureFallsBelowWarningLevelEventHandler;

    void RunHeatSensor();
}

public class TemperatureEventArgs : EventArgs
{
    public double Temperature { get; set; }
    public DateTime CurrentDateTime { get; set; }
}