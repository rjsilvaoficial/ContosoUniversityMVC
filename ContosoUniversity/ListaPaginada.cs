using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity
{
    public class ListaPaginada<T> : List<T>
    {
        public int PgAtual { get; private set; }
        public int QtdPorPg { get; private set; }
        public int QtdDeItens { get; private set; }
        public int QtdDePgs { get; private set; }

        public bool TemPgAnterior => PgAtual > 1;
        public bool TemPgPosterior => PgAtual < QtdDePgs;

        public ListaPaginada(List<T> itensAExibir, int pgAtual, int qtdPorPg, int qtdDeItens, int qtdDePgs)
        {
            PgAtual = pgAtual;
            QtdPorPg = qtdPorPg;
            QtdDeItens = qtdDeItens;
            QtdDePgs = qtdDePgs;

            this.AddRange(itensAExibir);
        }

        public static async Task<ListaPaginada<T>> InstanciarListaPaginada(IQueryable<T> itensAPaginar, int pgAtual, int qtdPorPg)
        {
            var qtdDeItens = await itensAPaginar.CountAsync();
            var itensAExibir = await itensAPaginar.Skip(pgAtual * qtdPorPg - qtdPorPg).Take(qtdPorPg).ToListAsync();
            var qtdDePgs = Convert.ToInt32(Math.Ceiling(qtdDeItens * 1M / qtdPorPg));
            return new ListaPaginada<T>(itensAExibir, pgAtual, qtdPorPg,  qtdDeItens, qtdDePgs);
        }





    }
}
