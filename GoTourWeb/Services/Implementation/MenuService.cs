using GoTourWeb.Helpers;
using GoTourWeb.Models;
using GoTourWeb.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Services.Implementation {
	public class MenuService : IMenuService {
		private ApiClient _vApiClient;

		public MenuService() {
            _vApiClient = new ApiClient(Constants.UrlBase);
		}

		
        public async Task<MenuViewModel> GetElements(string valIdLanguage) {
            MenuViewModel vResult = null;
            try {
                var vResponse = await _vApiClient.ExecuteGet<ClientResponseViewModel>("menu",valIdLanguage);
                if (vResponse == null) {
                    return vResult;
                }
                var vClientResponse = (ClientResponseViewModel)vResponse.Content;
                if (vClientResponse.Result == "False") {
                    vResult = null;
                } else {
                   vResult = JsonConvert.DeserializeObject<MenuViewModel>(vClientResponse.Result);
                }
            } catch (Exception vEx) {
                string vMensage = vEx.Message;
                vResult = null;
            }
            return vResult;
        }


        
    }
}
