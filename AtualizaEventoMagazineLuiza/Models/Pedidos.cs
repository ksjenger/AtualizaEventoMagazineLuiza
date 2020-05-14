using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtualizaEventoMagazineLuiza.Models
{
    public class Pedidos
    {
        public int Id { get; set; }
        public int Pedido { get; set; }

        public int NotaFiscal { get; set; }

        public string Etiqueta { get; set; }

        public int StatusEvento { get; set; }

        public DateTime DataOcorrencia { get; set; }
        public Pedidos()
        {
            Id = int.MinValue;
            Pedido = int.MinValue;
            NotaFiscal = int.MinValue;
            Etiqueta = string.Empty;
            StatusEvento = int.MinValue;
            DataOcorrencia = DateTime.MinValue;
        }
    }

    
}
