using MediatR;

namespace UserAPI.Features.Funds.Queries;

public record GetUserFundsQuery(int UserId) : IRequest<object?>;
