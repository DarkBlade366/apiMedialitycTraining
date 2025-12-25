using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiMedialityc.Features.Users.Endpoints
{
    public class DebugClaimsEndpoint : EndpointWithoutRequest<object>
    {
        public override void Configure()
        {
            Get("/debug-claims");
            AllowAnonymous();
            AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        }

        public override Task HandleAsync(CancellationToken ct)
        {
            var claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            });

            return Send.OkAsync(claims);
        }
    }
}