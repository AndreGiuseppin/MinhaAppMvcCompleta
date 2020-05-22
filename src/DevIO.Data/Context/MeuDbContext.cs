using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIO.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options)
        {

        }

        //PARA CADA MODEL, DEVERÁ ACRESCENTAR UMA NOVA LINHA 
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CASO NÃO SEJA DEFINIDO AS PROPRIEDADES DE UMA COLUNA, AQUI SE DEFINE O VALOR PADRÃO DELA
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }                

            // AQUI É MEPAEADO TODOS OS MAPPINGS FEITO
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            // AQUI PROTEGE OS DADOS DA TABELA CASO HAJA UM DELETE EM CASCADA
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }                

            base.OnModelCreating(modelBuilder);
        }
    }
}
