List<Operations> calculations = new List<Operations>
{
    new() { ThValue = "0", ThResult = "0", ThOperation = ""}
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

    if (calculationData.ThValue != "")
    {
        if (calculation.ThValue == "0")
            calculation.ThValue = "";

        if (calculation.ThValue == "" && calculationData.ThValue == ".")
            calculation.ThValue = "0.";
        else
            calculation.ThValue = calculation.ThValue + calculationData.ThValue;
    }
    else if (calculationData.ThOperation != "")
    {
        if (calculationData.ThOperation == "operator_delite")
        {
            calculation.ThValue = "0";
            calculation.ThResult = "0";
            calculation.ThOperation = "";
        }
    }

    return Results.Json(calculation);

});

app.Run();
public class Operations
{
    public string ThValue { get; set; } = "";
    public string ThResult { get; set; } = "";
    public string ThOperation { get; set; } = "";
}
