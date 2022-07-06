List<Operations> calculations = new List<Operations>
{
    new() { ThValue = "", ThResult = 0, ThOperation = ""}
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/calculations", () => calculations);

app.MapGet("/api/calculations", () =>
{

    Operations? calculation = calculations.FirstOrDefault();

    return Results.Json(calculation);

});

app.MapPut("/api/calculations", (Operations calculationData) =>
{

    var calculation = calculations.FirstOrDefault();

    calculation.ThValue = calculation.ThValue + calculationData.ThValue;

    return Results.Json(calculation);

});

app.Run();
public class Operations
{
    public string ThValue { get; set; } = "";
    public decimal ThResult { get; set; } = 0;
    public string ThOperation { get; set; } = "";
}
