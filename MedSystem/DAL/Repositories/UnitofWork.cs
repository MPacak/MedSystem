using DAL.IRepositories;
using DAL.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        private readonly MedicalSystemContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        
        //cache tako da nemoramo dok traje unit of work svaki put kreirati instancu nego spremi instance kad ih prvi put pozovem pa ih samo koristi
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public UnitofWork(MedicalSystemContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return ((Lazy<IRepository<T>>)_repositories.GetOrAdd(
        typeof(T),
        _ => new Lazy<IRepository<T>>(() => _repositoryFactory.CreateRepository<T>())
            )).Value;
        }

        public void Save()
        {
            _context.SaveChanges(); 
        }
    }
}
