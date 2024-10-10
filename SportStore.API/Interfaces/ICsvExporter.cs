using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.API.Entities;
using SportStore.Application.Features.Users.Commands.Queries;


namespace SportStore.API.Interfaces;

public interface ICsvExporter
{
    byte[] ExportUsersToCsv(List<User> users);
}