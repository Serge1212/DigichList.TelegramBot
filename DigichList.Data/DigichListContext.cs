using Microsoft.EntityFrameworkCore;

namespace DigichList.Data
{
    public class DigichListContext : DbContext
    {
        public DigichListContext(DbContextOptions<DigichListContext> options) : base(options)
        {

        }
    }
}
