using TemplateDotNet.Domains.Test;
using TemplateDotNet.Dtos.Test;
using TemplateDotNet.Migrations;

namespace TemplateDotNet.Repositories.Test;

public class UserRepository : CoreRepository<User, ApiDbContext>, IUserRepository
{
    public UserRepository(
        ApiDbContext context
    ): base(context)
    {
    }

    public async Task<List<UserDto>> GetUserList(CancellationToken cancellationToken)
    {
        var list = await FindAll(cancellationToken);
        return list
            .Select(t => new UserDto()
                {
                    Id = t.Id,
					Birthdate = t.Birthdate,
					FirstName = t.FirstName,
					LastName = t.LastName,
					CreatedAt = t.CreatedAt,
					UpdatedAt = t.UpdatedAt
                }
             )
            .ToList();
    }

    public async Task CreateUser(UserDto user, CancellationToken cancellationToken)
    => await CreateAsync(new User(user), cancellationToken);
}
