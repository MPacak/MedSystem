using DAL.IRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly MedicalSystemContext _context;

        public RepositoryFactory(MedicalSystemContext context)
        {
            _context = context;
        }
        public IRepository<T> CreateRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }
    }
}
