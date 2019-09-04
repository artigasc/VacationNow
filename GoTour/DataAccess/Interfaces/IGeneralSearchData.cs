using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTour.DataAccess.Interfaces
{
    interface IGeneralSearchData
    {
        string SearchText(GeneralSearch valSearchText);

    }
}
