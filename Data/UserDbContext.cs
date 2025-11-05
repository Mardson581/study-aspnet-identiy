using Learn.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Learn.Identity.Data;

public class UserDbContext : IdentityDbContext<ApplicationUser>
{

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    { }
}