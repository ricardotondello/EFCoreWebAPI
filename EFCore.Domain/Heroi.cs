using System.Collections.Generic;

namespace EFCore.Domain
{
    public class Heroi : BaseEntity
    {
        public string Nome { get; set; }
        public IdentidadeSecreta Identidade { get; set; }
        public ICollection<Arma> Armas { get; set; }

        public ICollection<Batalha> HeroisBatalhas { get; set; }
    }
}