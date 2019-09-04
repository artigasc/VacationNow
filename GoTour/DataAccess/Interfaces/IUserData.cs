using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoTour.DataAccess.Interfaces {
    public interface IUserData {
        string Insert(User valUser);
        string SelectUserWebById(Guid id);
        string SelectUserAll();
        string UpdateUser(User valUser);
        string UpdateStateUser(Guid Id, int State);
    }
}
