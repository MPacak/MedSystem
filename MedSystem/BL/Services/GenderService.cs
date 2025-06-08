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
    public class GenderService : IGenderService
    {
        private readonly IUnitofWork _unitOfWork;

        public GenderService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetGenderById(int id)
        {
            var repo = _unitOfWork.GetRepository<Gender>();
            var gender = repo.FindOne(x => x.Id == id);
            return gender.Type;
        }

        public int GetGenderIdByName(string name)
        {

            var repo = _unitOfWork.GetRepository<Gender>();
            var gender = repo.FindOne(t => t.Type == name);
            return gender.Id;
        }

        public IEnumerable<GenderDto> GetGenderTypeList()
        {
            var repo = _unitOfWork.GetRepository<Gender>();
            return repo.GetAll().Select(g => new GenderDto { Id = g.Id, Type = g.Type }).ToList();
        }
    }
}
