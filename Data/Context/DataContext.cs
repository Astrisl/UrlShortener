using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }

        public DataContext() : base(GetOptionsFromConnStr(string.Empty)) { }// get from appsettings.json

        public DataContext(DbContextOptions options) : base(options) { }

        public static DbContextOptions GetOptionsFromConnStr(string connStr)
        {
            DbContextOptionsBuilder<DataContext> optBuilder = new();

            optBuilder.UseNpgsql(connStr);

            return optBuilder.Options;
        }
    }
}
