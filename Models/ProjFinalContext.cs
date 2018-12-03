using Microsoft.EntityFrameworkCore;

namespace ProjetoFinal.Models
{
    public class ProjFinalContext : DbContext
    {
        public ProjFinalContext (DbContextOptions<ProjFinalContext> options)
            : base(options)
        {
        }
        public DbSet<ProjetoFinal.Models.AnaliseQualidade> AnaliseQualidade { get; set; }
        public DbSet<ProjetoFinal.Models.Cliente> Cliente { get; set; }
        public DbSet<ProjetoFinal.Models.Embalagem> Embalagem { get; set; }

        public DbSet<ProjetoFinal.Models.Fornecedor> Fornecedor { get; set; }
        
        public DbSet<ProjetoFinal.Models.Ordem> Ordem { get; set; }
          public DbSet<ProjetoFinal.Models.Produto> Produto { get; set; }


    }
}