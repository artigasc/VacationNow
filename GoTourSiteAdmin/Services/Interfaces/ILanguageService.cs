﻿using GoTourSiteAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoTourSiteAdmin.Services.Interfaces {
    public interface ILanguageService {
        Task<List<Language>> GetLanguage();
    }
}
