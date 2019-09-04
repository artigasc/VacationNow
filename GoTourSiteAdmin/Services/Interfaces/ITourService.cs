using GoTourSiteAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Interfaces {
    public interface ITourService {
        Task<List<TourViewModel>> GetTour();
        Task<List<TourViewModel>> GetTourById(Guid vId);
        Task<ClientResponseViewModel> AddTour(TourViewModel vModel);
        Task<ClientResponseViewModel> UpdateTour(TourViewModel vModel);
        Task<ClientResponseViewModel> ChangeState(TourViewModel vModel);
    }
}
