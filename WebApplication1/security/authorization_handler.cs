using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.security
{

    public class authorization_handler : AuthorizationHandler<authorization_requirement>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public authorization_handler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, authorization_requirement requirement)
        {
            try
            {

                var Request = _httpContextAccessor.HttpContext.Request;
                var auth_string = Request.Headers.Where(p => p.Key == "Authorization").FirstOrDefault().Value;

                context.Succeed(requirement);
            }
            catch (Exception ex)
            {

            }

            return Task.CompletedTask;
        }
    }
}
