using QBT_Invoice_Generator.Services;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
QuestPDF.Settings.License = LicenseType.Community;
var fontCourierNewPath = Path.Combine(Directory.GetCurrentDirectory() + "/Verdana.ttf");

using (var fontFileStream = File.OpenRead(fontCourierNewPath))
{
    QuestPDF.Drawing.FontManager.RegisterFontWithCustomName("Verdana", fontFileStream);
} 

Console.WriteLine();
builder.Services.AddControllers();

builder.Services.AddScoped<InvoiceGeneratorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
