using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SportStore.API.Behaviors;

// Only for Clean Arch

// public class ExampleBehavior : IPipelineBehavior<DeleteUserCommand, ErrorOr<User>>
// {
//     IEnumerable<User> users = default!;
//     private readonly ILogger<ExampleBehavior> _logger;
//     private readonly IUserRepository _userRepo;

//     public ExampleBehavior(ILogger<ExampleBehavior> logger, IUserRepository userRepo)
//     {
//          System.Console.WriteLine("Поведение конструктор!!!");
//         _logger = logger;
//         _userRepo = userRepo;
//     }
//     public async Task<ErrorOr<User>> Handle(DeleteUserCommand request, RequestHandlerDelegate<ErrorOr<User>> next, CancellationToken cancellationToken)
//     {
//        System.Console.WriteLine("Поведение обработчика!!!");
//        users = await _userRepo.GetUsers();
//        _logger.LogWarning($"Количество пользователей было: {users.Count()}");
//        var response = await next();

//        users = await _userRepo.GetUsers();
//        _logger.LogWarning($"Количество пользователей стало: {users.Count()}");
//        return response;
//     }

// }