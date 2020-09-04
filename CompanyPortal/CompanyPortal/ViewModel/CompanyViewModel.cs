using CompanyPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyPortal.ViewModel
{
    public class CompanyViewModel
    {
        public Company Company { get; set; }
        public bool IsFavourite { get; set; }
    }
}
