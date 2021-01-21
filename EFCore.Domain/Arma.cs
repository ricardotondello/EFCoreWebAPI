namespace EFCore.Domain
{
    public class Arma : BaseEntity
    {
        public string Nome { get; set; }
        public Heroi Heroi { get; set; }
        public int HeroiId { get; set; }
    }
}