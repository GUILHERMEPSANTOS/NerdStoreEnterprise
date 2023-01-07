using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DomainObjects.Data;
using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    { 
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid id);
        void Add(Produto produto);
        void Update(Produto produto);
    }
}