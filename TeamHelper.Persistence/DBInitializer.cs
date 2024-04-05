
namespace TeamHelper.Persistence
{
    public class DBInitializer
    {
        public static void Initialize(TeamHelperDBContext context)
        {
           context.Database.EnsureCreated();
        }
    }
}
