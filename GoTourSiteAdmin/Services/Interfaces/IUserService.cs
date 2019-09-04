using GoTourSiteAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Interfaces {
    public interface IUserService {
        Task<ClientResponseViewModel> UpdateState(UserViewModel valUser);
        Task<ClientResponseViewModel> Update(UserViewModel valUser);
        Task<List<UserViewModel>> GetUser();
        Task<UserViewModel> GetUserById(Guid Id);

    }
}
