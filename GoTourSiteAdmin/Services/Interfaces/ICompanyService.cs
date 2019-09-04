using GoTourSiteAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Interfaces {
    public interface ICompanyService {
        Task<List<CompanyViewModel>> GetCompany();
        Task<CompanyViewModel> GetCompanyById(Guid valId);
        Task<ClientResponseViewModel> Register(CompanyViewModel vModel);
        Task<ClientResponseViewModel> ChangeState(CompanyViewModel vModel);
        Task<ClientResponseViewModel> Update(CompanyViewModel vModel);

    }
}
