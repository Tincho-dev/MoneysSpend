using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository;

public class RecordContext : DbContext
{
    public RecordContext(DbContextOptions<RecordContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<SpendRecord> SpendRecords => Set<SpendRecord>();
    public DbSet<Category> Categories => Set<Category>();
}
