using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}