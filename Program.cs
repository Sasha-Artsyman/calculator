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
        else if ((calculationData.ThValue == "." && calculation.ThValue.IndexOf(".") >= 0) || (calculation.ThValue == "0" && calculationData.ThValue == "0"))
            calculation.ThValue = calculation.ThValue;
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
        else
        {
            if (calculation.ThOperation == "operator_plus")
            {
                calculation.ThResult = Convert.ToString(Convert.ToDecimal(calculation.ThResult.Replace(".", ",")) + Convert.ToDecimal(calculation.ThValue.Replace(".", ",")));
                calculation.ThResult = calculation.ThResult.Replace(",", ".");
                calculation.ThValue = "0";
            }
            else if (calculation.ThOperation == "operator_minus")
            {
                calculation.ThResult = Convert.ToString(Convert.ToDecimal(calculation.ThResult.Replace(".", ",")) - Convert.ToDecimal(calculation.ThValue.Replace(".", ",")));
                calculation.ThResult = calculation.ThResult.Replace(",", ".");
                calculation.ThValue = "0";
            }
            else if (calculation.ThOperation == "operator_multiply")
            {
                calculation.ThResult = Convert.ToString(Math.Round(Convert.ToDecimal(calculation.ThResult.Replace(".", ",")) * Convert.ToDecimal(calculation.ThValue.Replace(".", ",")), 3)).Replace(",", ".");
                calculation.ThValue = "0";
            }
            else if (calculation.ThOperation == "operator_divide" && Convert.ToDecimal(calculation.ThValue) != 0)
            {
                calculation.ThResult = Convert.ToString(Math.Round(Convert.ToDecimal(calculation.ThResult.Replace(".", ",")) / Convert.ToDecimal(calculation.ThValue.Replace(".", ",")), 3)).Replace(",", ".");
                calculation.ThValue = "0";
            }
            else if (calculation.ThOperation != "operator_result")
            {
                calculation.ThResult = calculation.ThValue;
                calculation.ThValue = "0";
            }
            calculation.ThOperation = calculationData.ThOperation;
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
