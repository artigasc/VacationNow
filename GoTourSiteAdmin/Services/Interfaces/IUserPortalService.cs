using GoTourSiteAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Interfaces {
    public interface IUserPortalService {
        Task<ClientResponseViewModel> Register(UserPortalViewModel valUser);
        Task<List<UserPortalViewModelResponse>> GetUser();
        Task<ClientResponseViewModel> ChangeState(UserPortalViewModel valUser);
        Task<UserPortalViewModelResponse> GetUserById(string Id);
        Task<ClientResponseViewModel> Update(UserPortalViewModel valUser);
    }
}
