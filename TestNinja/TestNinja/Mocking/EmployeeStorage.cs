namespace TestNinja.Mocking
{
    public interface IEmployeeStorage
    {
        void Delete(int id);
    }

    public class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext _db;

        public EmployeeStorage()
        {
            _db = new EmployeeContext();
        }

        /*This method should not be Unit tested as it interacts directly with an external resource. Instead, it should be tested via an Integration test. */
        public void Delete(int id)
        {
            var employee = _db.Employees.Find(id);

            if (employee == null) return;

            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}
