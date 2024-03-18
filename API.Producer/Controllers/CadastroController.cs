using Bogus;
using Data.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace API.Producer.Controllers
{
    [ApiController]
    [Route("/Cadastro")]
    public class CadastroController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        private static Faker<Usuario>? _usuarioFaker;
        private static int _counter = 1;
        private const string CULTURE_CODE = "pt_BR";

        public CadastroController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar()
        {
            //Obtém o nome da fila do broker do arquivo appsettings.json
            var queue = _configuration.GetSection("MassTransit").GetValue<string>("Queue") ?? string.Empty;
            //Define o endpoint do broker
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{queue}"));
            //Envia a mensagem gerada para a fila do broker
            await endpoint.Send(new Cadastro(_counter++, GerarUsuarioAleatorio()));

            return Ok();
        }

        private static Usuario GerarUsuarioAleatorio()
        {
            //Caso o objeto Usuario faker esteja nulo, cria uma nova instância com os parâmetros a serem utilizados para gerar os valores
            if (_usuarioFaker == null)
            {
                //Instancia um Usuario faker com as regras abaixo
                _usuarioFaker = new Faker<Usuario>(CULTURE_CODE)
                .RuleFor(n => n.Id, f => f.IndexFaker)
                .RuleFor(n => n.Nome, f => f.Name.FullName())
                .RuleFor(n => n.CPF, f => f.Random.Long(10000000000, 99999999999))
                .RuleFor(n => n.Email, f => f.Internet.Email(f.Person.FirstName).ToLower());
            }

            //Gera um usuário fake
            return _usuarioFaker.Generate(1).First();
        }
    }
}
