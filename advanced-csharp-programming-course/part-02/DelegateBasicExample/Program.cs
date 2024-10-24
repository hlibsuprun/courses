namespace DelegateBasicExample;

internal class Program
{
    private static void Main(string[] args)
    {
        var log = new Log();

        LogDel logTextToScreenDel, logTextToFileDel;

        logTextToScreenDel = log.LogTextToScreen;
        logTextToFileDel = log.LogTextToFile;

        var multiLogDel = logTextToScreenDel + logTextToFileDel;

        Console.WriteLine("Please enter your name: ");

        var name = Console.ReadLine();

        LogText(multiLogDel, name);
    }

    private static void LogText(LogDel logDel, string text)
    {
        logDel(text);
    }

    public class Log
    {
        public void LogTextToScreen(string text)
        {
            Console.WriteLine($"{DateTime.Now}: {text}");
        }

        public void LogTextToFile(string text)
        {
            using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now}: {text}");
            }
        }
    }

    private delegate void LogDel(string text);
}