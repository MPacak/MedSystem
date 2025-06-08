using BL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IServices
{
    public interface IGenderService
    {
        int GetGenderIdByName(string name);
        IEnumerable<GenderDto> GetGenderTypeList();
        string GetGenderById(int id);
    }
}
