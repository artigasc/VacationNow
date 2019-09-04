﻿using GoTourWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourWeb.Services.Interfaces {
    public interface IGeneralSearchService {
        Task<List<GeneralResultViewModel>> SearchPrincipal(GeneralSearchViewModel valSearchText);
    }
}
