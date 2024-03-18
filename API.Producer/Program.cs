using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

// Configure MassTransit / RabbitMq Settings
var massTransit = configuration.GetSection("MassTransit");
var queue = massTransit?["Queue"] ?? string.Empty;
var server = massTransit?["Server"] ?? string.Empty;
var virtualHost = massTransit?["VirtualHost"] ?? string.Empty;
var user = massTransit?["User"] ?? string.Empty;
var password = massTransit?["Password"] ?? string.Empty;

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(server, virtualHost, u =>
        {
            u.Username(user);
            u.Password(password);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
