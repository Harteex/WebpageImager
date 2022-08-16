using WebpageImager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var chromeServiceConfig = builder.Configuration.GetSection("ChromeDriver");
var chromeService = new ChromeService(chromeServiceConfig["ExecutablePath"], chromeServiceConfig["WindowSize"], chromeServiceConfig["Url"]);
builder.Services.AddSingleton(chromeService);
builder.Services.AddHostedService(_ => chromeService);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
