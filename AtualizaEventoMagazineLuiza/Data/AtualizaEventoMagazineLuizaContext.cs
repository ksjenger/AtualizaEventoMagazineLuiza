using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AtualizaEventoMagazineLuiza.Models
{
    public class AtualizaEventoMagazineLuizaContext : DbContext
    {
        public AtualizaEventoMagazineLuizaContext (DbContextOptions<AtualizaEventoMagazineLuizaContext> options)
            : base(options)
        {
        }

        public DbSet<AtualizaEventoMagazineLuiza.Models.MontarRelatorios> MontarRelatorios { get; set; }
    }
}
