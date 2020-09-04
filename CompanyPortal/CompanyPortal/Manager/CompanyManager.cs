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

        /// <summary>
        /// Get companies
        /// </summary>
        /// <returns> list of companies</returns>
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        /// <summary>
        /// Adds company
        /// <param name="company">Smtp server name</param>
        /// </summary>
        /// <returns> returns company</returns>
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

        /// <summary>
        /// Get companies
        /// </summary>
        /// <returns> list of companies</returns>
        public List<CompanyViewModel> GetCompanies(long userId, string sortOrder, string searchString)
        {
            try
            {
                List<CompanyViewModel> companies = _context.Companies.Select(c => new CompanyViewModel
                {
                    Company = c,
                    IsFavourite = _context.Favourites.Any(f => f.UserId == userId && f.CompanyId == c.CompanyId)
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

        // <summary>
        /// Adds company into the favourite
        /// </summary>
        /// <returns> true or false</returns>
        public async Task<bool> AddFavourite(FavouriteViewModel favourite)
        {
            try
            {
                Favourites favouriteCompany = new Favourites();
                favouriteCompany.CompanyId = favourite.CompanyId;
                favouriteCompany.UserId = favourite.UserId;
                _context.Favourites.Add(favouriteCompany);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Delets company from the favourite
        /// </summary>
        /// <returns> true or false</returns>
        public async Task<bool> DeleteFavourite(long userId, long companyId)
        {
            try
            {
                var result = _context.Favourites.Where(f => f.CompanyId == companyId && f.UserId == userId).FirstOrDefault();
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
