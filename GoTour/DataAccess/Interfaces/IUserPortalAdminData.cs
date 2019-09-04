using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTour.DataAccess.Interfaces {
    interface IUserPortalAdminData {
        string VerifyUserPortal(string vUser, string vPassword);
        Task<string> InsertUserPortal(UserPortalAdmin valUser);
        string SelectUserPortalAll();
        string SelectUserPortalById(Guid Id);
        string UpdateUserPortal(UserPortalAdmin valUserPortal);
        string UpdateStateUserPortal(UserPortalAdmin valUserPortal);

    }
}
