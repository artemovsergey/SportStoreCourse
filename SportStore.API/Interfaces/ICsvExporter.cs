using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Application.Features.Users.Commands.Queries;
using SportStore.Domain;

namespace SportStore.Application.Interfaces
{
    public interface ICsvExporter
    {
        byte[] ExportUsersToCsv(List<User> users);
    }
}