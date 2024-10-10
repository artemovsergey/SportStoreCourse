using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace SportStore.Application.Features.Users.Commands.Queries
{
    public class GetUsersExportQuery : IRequest<UserExportViewModel>
    {
        
    }

    public class UserExportViewModel{
        public string? UserExportFileName {get ;set;}
        public string? ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}