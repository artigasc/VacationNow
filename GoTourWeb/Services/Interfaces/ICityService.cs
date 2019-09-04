using GoTourWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Services.Interfaces {
	public interface ICityService {
        Task<TourResponseViewModel> GetTours(FilterToursViewModel valFilter);
      
    }
}
