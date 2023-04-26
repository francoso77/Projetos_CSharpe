using AppSales.Data;
using AppSales.Models;

namespace AppSales.Services
{
    public class DepartmentService
    {
        private readonly AppSalesContext _context;

        public DepartmentService(AppSalesContext context)
        {
            _context = context; 
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(dep => dep.Name).ToList();
        }
    }
}
