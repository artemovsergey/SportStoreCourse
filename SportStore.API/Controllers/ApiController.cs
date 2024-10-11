using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SportStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {

      protected readonly IMediator _mediator;    
      protected ApiController(IMediator mediator)
      {
         _mediator = mediator;
      }

    }
}