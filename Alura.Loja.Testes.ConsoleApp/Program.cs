using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (LojaContext contexto = new LojaContext())
            {
                IServiceProvider serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                ILoggerFactory loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create());

                List<Produto> produtos = contexto.Produtos.ToList();
                foreach (Produto p in produtos)
                {
                    Console.WriteLine(p);
                }

                ExibeEntries(contexto.ChangeTracker.Entries());

                //var p1 = produtos.Last();
                //p1.Nome = "Herry Potter";

                //Produto novoProduto = new Produto()
                //{
                //    Nome = "Desinfetante",
                //    Categoria = "Limpeza",
                //    Preco = 2.99
                //};
                //contexto.Produtos.Add(novoProduto);

                var p1 = produtos.First();
                contexto.Produtos.Remove(p1);

                ExibeEntries(contexto.ChangeTracker.Entries());

                contexto.SaveChanges();

                ExibeEntries(contexto.ChangeTracker.Entries());
            }
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("===================");
            foreach (var e in entries)
            {
                Console.WriteLine(e.Entity.ToString() + " - " + e.State);
            }
        }
    }
}
