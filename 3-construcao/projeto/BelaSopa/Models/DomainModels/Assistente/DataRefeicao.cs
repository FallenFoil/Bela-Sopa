using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BelaSopa.Models.DomainModels.Assistente {
    public class DataRefeicao {
        public DataRefeicao() { }
        public DataRefeicao(int dia, bool almoco) {
            this.Dia = dia;
            this.Almoco = almoco;
        }

        public int DataRefeicaoId { get; set; }
        public int Dia { set; get; }
        public bool Almoco { set; get; }

        public bool IsAlmoco() {
            return Almoco;
        }

        public bool IsJantar() {
            return !IsAlmoco();
        }

        public bool Equals(DataRefeicao data) {
            return this.Dia == data.Dia && this.Almoco == data.Almoco;
        }

        public override int GetHashCode() {
            return this.DataRefeicaoId.GetHashCode();
        }
    }
}
