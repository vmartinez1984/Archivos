using Files.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Files.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeUserFilter: Attribute, IAuthorizationFilter
    {
        private AppDbContext _dbContext;

        public AuthorizeUserFilter()
        {

        }

        public AuthorizeUserFilter(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {            
            throw new System.NotImplementedException();
        }
    }
}
