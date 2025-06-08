using BL.DTO;
using BL.IServices;
using DAL.IRepositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IUnitofWork _unitofWork;

        public DiseaseService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public int GetDIseaseIdByName(string name)
        {
           var repo = _unitofWork.GetRepository<Disease>();
            var disease = repo.FindOne(d => d.Name == name);
            return disease.Id;
        }

        public string GetDiseaseNameById(int id)
        {
            var disease = _unitofWork.GetRepository<Disease>().FindOne(d => d.Id == id);
            return disease.Name;
        }

        public IEnumerable<DiseaseDto> GetDiseaseNameList()
        {
            var repo = _unitofWork.GetRepository<Disease>().GetAll();
            return repo.Select(d => new DiseaseDto { Id = d.Id, Name = d.Name }).ToList();
        }
    }
}
