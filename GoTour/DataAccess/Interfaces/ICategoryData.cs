﻿using GoTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoTour.DataAccess.Interfaces {
    public interface ICategoryData {
        string SelectAllByLanguage(string valIdLanguage);
    }
}
