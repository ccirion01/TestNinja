using System.Linq;

namespace TestNinja.Mocking
{
    public interface IHousekeeperRepository
    {
        IQueryable<Housekeeper> GetAll();
    }

    public class HousekeeperRepository : IHousekeeperRepository
    {
        public IQueryable<Housekeeper> GetAll()
        {
            return new UnitOfWork().Query<Housekeeper>();
        }
    }
}
