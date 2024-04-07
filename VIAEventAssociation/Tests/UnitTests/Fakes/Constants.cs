namespace UnitTests.Fakes;

public class Constants
{
    public static readonly DateTime TEST_DATE = new DateTime(2024, 4, 1, 17, 0, 0);
    public static readonly string START_DATE_STRING = TEST_DATE.ToString("yyyy-MM-dd HH:mm:ss");
    public static readonly string END_DATE_STRING = TEST_DATE.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss");
}