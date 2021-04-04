using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class NPDataContext : IdentityDbContext
{
    public NPDataContext(DbContextOptions<NPDataContext> options)
        : base(options)
    { }
}
