using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtualizaEventoMagazineLuiza.Models
{
    public class MontarRelatorios
    {
        public int IdRemetente { get; set; }
        public int IdUnidadePostadora { get; set; }
        public int StFormatoRetorno { get; set; }
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }

        public MontarRelatorios()
        {
            IdRemetente = 24419;
            StFormatoRetorno = 10015;
        }

    }
}
