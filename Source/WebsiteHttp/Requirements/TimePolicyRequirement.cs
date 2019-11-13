using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace WebsiteHttp.Requirements
{
    public class TimePolicyRequirement : IAuthorizationRequirement
    {
        public TimePolicyRequirement(string time)
        {
            Time = time;
        }

        public string Time { get; }
    }

    public class TimePolicyAuthorizationHandler : AuthorizationHandler<TimePolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TimePolicyRequirement requirement)
        {
            var timeClaim = context.User.Claims.Single(clm => clm.Type == "Time");

            if (timeClaim.Value.CompareTo(requirement.Time) == 1)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
