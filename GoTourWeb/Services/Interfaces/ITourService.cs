using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoTourWeb.Models;

namespace GoTourWeb.Services.Interfaces {
    public interface ITourService {
        Task<List<TourViewModel>> GetToursByRanking(string valIdLanguage);
    }
}
