using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoTour.DataAccess.Interfaces {
    public interface ICityData {
        string SelectByLanguage(string valIdLanguage);
        string SelectById(Guid vId);
        string SelectCityAll();
        Task<string> Insert(ListCityLanguage vCity);
        Task<string> Update(ListCityLanguage vCity);
        string UpdateState(City vCity);

    }
}
