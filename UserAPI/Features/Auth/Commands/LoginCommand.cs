using MediatR;

namespace UserAPI.Features.Auth.Commands;

public record LoginCommand(string Username, string Password) : IRequest<object?>;
