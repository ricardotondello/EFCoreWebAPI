using System;
using System.Collections.Generic;

namespace EFCore.Domain
{
    public class Batalha : BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }

        public ICollection<Batalha> HeroisBatalhas { get; set; } 
    }
}