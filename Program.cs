string thisValue = "";
decimal thisResult = 0;
string thisOperation = "";

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
