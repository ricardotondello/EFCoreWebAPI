namespace EFCore.Domain
{
    public class IdentidadeSecreta : BaseEntity
    {
        public string NomeReal { get; set; }
        public int HeroiId { get; set; }
        public Heroi Heroi { get; set; }
    }
}