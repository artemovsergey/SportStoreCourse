using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Application.Models;

namespace SportStore.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);   
    }
}