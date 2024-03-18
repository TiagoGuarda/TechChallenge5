using Data.Entities;
using MassTransit;

namespace Worker.Consumer.Events
{
    public class CadastroConsumidor : IConsumer<Cadastro>
    {
        public Task Consume(ConsumeContext<Cadastro> context)
        {
            Console.WriteLine(context.Message);
            return Task.CompletedTask;
        }
    }
}
