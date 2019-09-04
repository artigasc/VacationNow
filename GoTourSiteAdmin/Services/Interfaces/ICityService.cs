using GoTourSiteAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Interfaces {
    public interface ICityService {
        Task<List<CityViewModel>> GetCity();
        Task<CityLanguageViewModel> GetCityById(Guid vId);
        Task<ClientResponseViewModel> AddCity(CityLanguageViewModel vModel);
        Task<ClientResponseViewModel> EditCity(CityLanguageViewModel vModel);
        Task<ClientResponseViewModel> ChangeState(CityViewModel vModel);
    }
}
