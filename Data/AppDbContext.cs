using Microsoft.EntityFrameworkCore;

namespace Revisao{

    public class AppDbContext : DbContext{
        //DbSet indica que Produto será uma tabela
        public DbSet<Produto> Produtos => Set<Produto>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if (!optionsBuilder.IsConfigured){
                var connectionString = 
                "server=localhost;database=revisao;user=root;password=";

                optionsBuilder.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                );
            }
        }


    }
}