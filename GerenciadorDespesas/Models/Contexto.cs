using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDespesas.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Meses> Meses { get; set; }

        public DbSet<Salarios> Salarios { get; set; }

        public DbSet<Despesas> Despesas { get; set; }

        public DbSet<TipoDespesas> TipoDespesas { get; set; }


        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
