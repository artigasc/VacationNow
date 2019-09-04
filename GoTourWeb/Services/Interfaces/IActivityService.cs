using GoTourWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Services.Interfaces {
	public interface IActivityService {
        Task<ActivityResponseViewModel> GetActivities(FilterActivityViewModel valFilter);
        Task<ActivityCompanyViewModel> GetActivityCompany(FilterActivityCompanyViewModel valFilter);
    }
}
