using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SportStore.Application.Interfaces;
using SportStore.Domain;

namespace SportStore.Application.Features.Users.Commands.Queries
{
    public class GetUsersExportHandler : IRequestHandler<GetUsersExportQuery, UserExportViewModel>
    {

        private readonly ICsvExporter _csvExporter;
        private readonly IUserRepository _userRepository;
        public GetUsersExportHandler(ICsvExporter csvExporter, IUserRepository userRepository)
        {
            _csvExporter = csvExporter;
            _userRepository = userRepository;
        }

        public async Task<UserExportViewModel> Handle(GetUsersExportQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsers();

            var fileData = _csvExporter.ExportUsersToCsv(users as List<User>);


            return new UserExportViewModel(){ ContentType = "text/csv",
                                              Data = fileData,
                                              UserExportFileName = "Users.csv"};
        }
    }
}