using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoTour.DataAccess.Interfaces {
    public interface ICompanyData {
        string SelectAll();
        string SelectById(Guid vId);
        bool Insert(Company vCompany);
        bool Update(Company vCompany);
        Company SelectCompanyById(Guid valId);
        Company SelectCompanyByPayment(Guid valId);
    }
}
