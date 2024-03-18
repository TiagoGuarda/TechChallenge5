namespace Data.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public long CPF { get; set; }
        public string? Email { get; set; }

        public Usuario() { }
    }
}
