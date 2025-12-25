using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Queries;
using ApiMedialityc.Features.Users.Validations;
using FastEndpoints;

namespace ApiMedialityc.Features.Users.Endpoints.Admin
{
    public class GetByIdUserEndpoint
        : Endpoint<GetByIdUserRequestDto, GetByIdUserResponseDto>
    {
        public override void Configure()
        {
            Get("/users/{id}");
            Roles("Admin");
            Validator<GetIdUserValidation>();
            Summary(s =>
            {
                s.Summary = "Recibe un usuario al proporcionarle su id";
                s.Description = "Recibe el usuario correspondiente al id proporcionado, sirve para hacer el update despues con el usuario que se quiera";
                s.ExampleRequest = new GetByIdUserRequestDto{}; 
            });
        }
        
        public override async Task HandleAsync(GetByIdUserRequestDto req, CancellationToken ct)
        {   
            req.Id = Route<Guid>("id");

            var query = new GetByIdUserQuery(req);
            var response = await query.ExecuteAsync(ct);
            await Send.OkAsync(response, ct);
        }
    }
}