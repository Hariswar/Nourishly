using MediatR;

namespace UserAPI.Features.Auth.Commands;

public record RegisterCommand(string FirstName, string LastName, string Email, string Username, string Password) : IRequest<object>;
