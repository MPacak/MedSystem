using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateRepository<T>() where T : class;
    }
}
