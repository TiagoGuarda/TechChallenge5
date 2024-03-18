using MassTransit;
using Worker.Consumer.Events;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        // Configure MassTransit / RabbitMq Settings
        var massTransit = configuration.GetSection("MassTransit");
        var queue = massTransit?["Queue"] ?? string.Empty;
        var server = massTransit?["Server"] ?? string.Empty;
        var virtualHost = massTransit?["VirtualHost"] ?? string.Empty;
        var user = massTransit?["User"] ?? string.Empty;
        var password = massTransit?["Password"] ?? string.Empty;

        services.AddHostedService<Worker.Consumer.Worker>();

        services.AddMassTransit(x => 
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(server, virtualHost, u => 
                {
                    u.Username(user);
                    u.Password(password);
                });

                cfg.ReceiveEndpoint(queue, e => e.Consumer<CadastroConsumidor>());
                cfg.ConfigureEndpoints(context);
            });

            x.AddConsumer<CadastroConsumidor>();
        });
    })
    .Build();

host.Run();
