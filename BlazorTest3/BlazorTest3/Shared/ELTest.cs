namespace BlazorTest3.Shared;

public class ELTest
{
    public string? Name { get; set; }

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string? Summary { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class ELTestRun
{
    public string? Id { get; set; }
    public string? Date { get; set; }
    public string? DeviceId { get; set; }
    public string? DeviceType { get; set; }
    public string? Errorscount { get; set; }
}