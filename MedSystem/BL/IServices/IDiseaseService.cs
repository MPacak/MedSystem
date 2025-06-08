using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IServices
{
    public interface IDiseaseService
    {
        int GetDIseaseIdByName(string name);
        IEnumerable<DiseaseDto> GetDiseaseNameList();
        string GetDiseaseNameById(int id);
    }
}
