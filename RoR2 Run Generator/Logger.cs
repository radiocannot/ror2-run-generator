namespace RoR2RunGenerator;

public static class Logger
{
    public static void LogException(Exception exception)
    {
        File.AppendAllText("log.txt",$"{exception}\r\n");
    }
}