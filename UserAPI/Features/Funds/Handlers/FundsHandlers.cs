using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Features.Funds.Queries;

namespace UserAPI.Features.Funds.Handlers;

public class GetUserFundsHandler : IRequestHandler<GetUserFundsQuery, object?>
{
    private readonly UserContext _context;

    public GetUserFundsHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<object?> Handle(GetUserFundsQuery request, CancellationToken cancellationToken)
    {
        var fundMap = await _context.Database
            .SqlQuery<UserFundMap>($"SELECT user_id AS \"UserId\", fund_id AS \"FundId\" FROM user_fund_map WHERE user_id = {request.UserId}")
            .FirstOrDefaultAsync(cancellationToken);

        if (fundMap == null) return null;

        var fund = await _context.Funds.FindAsync(new object[] { fundMap.FundId }, cancellationToken);
        if (fund == null) return null;

        return new { mealSwipes = fund.MealSwipes, dbds = fund.Dbds };
    }
}

public class UserFundMap { public int UserId { get; set; } public int FundId { get; set; } }
