using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyPortal.Context;
using CompanyPortal.Manager;
using CompanyPortal.Models;
using CompanyPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace CompanyPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyManager _companyManager;

        public CompanyController(CompanyManager companyManager)
        {
            _companyManager = companyManager;
        }

        [HttpGet]
        public ActionResult GetCompanies(long userId, string sortOrder = "", string searchString = "")
        {
            List<CompanyViewModel> companies = _companyManager.GetCompanies(userId, sortOrder, searchString);
            if (companies != null)
            {
                return Ok(companies);
            }

            return NotFound();
        }

        [HttpPost("company")]
        public async Task<ActionResult> AddCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                Company result = await _companyManager.AddCompanyAsync(company);
                if (result != null)
                {
                    return Ok(new ResponseModel { Status = "ok", Message = "Company added successfully" });
                }
            }

            return BadRequest(new ResponseModel { Status = "Failed", Message = "Model not valid" });
        }

        [HttpPost("favourite")]
        public async Task<ActionResult> AddFavouriteCompany(FavouriteViewModel favourite)
        {
            bool result = await _companyManager.AddFavourite(favourite);
            if (result)
            {
                return Ok(new ResponseModel { Status = "ok", Message = "Added company to favourite list" });
            }


            return BadRequest();
        }

        [HttpDelete("favourite/{userId}/{companyId}")]
        public async Task<ActionResult> DeleteFavourite(long userId, long companyId)
        {
            bool result = await _companyManager.DeleteFavourite(userId, companyId);
            if (result)
            {
                return Ok(new ResponseModel { Status = "ok", Message = "Deleted company from favourite list" });
            }


            return BadRequest();
        }
    }
}
