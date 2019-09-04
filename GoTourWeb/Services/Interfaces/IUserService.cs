using GoTourWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Services.Interfaces {
	public interface IUserService {
		Task<UserViewModel> Login(string vUsername, string vPassword);
        Task<ClientResponseViewModel> Register(UserViewModel valUser);
    }
}
