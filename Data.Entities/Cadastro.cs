namespace Data.Entities
{
    public class Cadastro
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataCadastro { get; set; }

        public Cadastro(int id, Usuario usuario) 
        { 
            Id = id;
            Usuario = usuario;
            DataCadastro = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Cadastro Id: {Id}, Usuário: {Usuario.Nome}, CPF: {Usuario.CPF.ToString(@"000\.000\.000\-00")}, Email: {Usuario.Email}, Data de Cadastro: {DataCadastro:dd/MM/yyyy}";
        }
    }
}
