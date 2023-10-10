using Finik.AuthService.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Finik.AuthService.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Role _role;

        public AuthRoleAttribute(Role role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) 
            { 
                return; 
            }

            if (!context.HttpContext.User.IsInRole(_role.ToString()))
            {
                context.Result = new ForbidResult();
            }               
        }
    }
}
