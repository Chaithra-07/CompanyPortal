using CompanyPortal.Context;
using CompanyPortal.Models;
using CompanyPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyPortal.Manager
{
    public class CompanyManager
    {
        private readonly CompanyContext _context;

        public CompanyManager(CompanyContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> AddCompanyAsync(Company company)
        {
            try
            {
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                return company;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CompanyViewModel> GetCompanies(long userId, string sortOrder, string searchString)
        {
            try
            {
                List<CompanyViewModel> companies = _context.Companies.Select(c => new CompanyViewModel
                {
                    Company = c,
                    IsFavourite = _context.Favourites.Any(f => f.User.UserId == userId && f.Company.CompanyId == c.CompanyId)
                }).ToList();


                if (companies != null)
                {
                    if (!String.IsNullOrEmpty(searchString))
                        companies = companies.Where(c => c.Company.Name.Contains(searchString)).ToList();

                    if (!String.IsNullOrEmpty(sortOrder))
                    {
                        switch (sortOrder)
                        {
                            case "ascending":
                                companies = companies.OrderBy(c => c.Company.Name).ToList();
                                break;
                            case "descending":
                                companies = companies.OrderByDescending(c => c.Company.Name).ToList();
                                break;
                        }
                    }
                }

                return companies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddFavourite(FavouriteViewModel favourite)
        {
            try
            {
                Favourites favouriteCompany = new Favourites();
                favouriteCompany.Company.CompanyId = favourite.CompanyId;
                favouriteCompany.User.UserId = favourite.UserId;
                _context.Favourites.Add(favouriteCompany);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteFavourite(long userId, long companyId)
        {
            try
            {
                var result  = _context.Favourites.Where(f => f.Company.CompanyId == companyId && f.User.UserId == userId).FirstOrDefault();
                _context.Entry(result).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
