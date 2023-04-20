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
            //usando o LINQ ordenamos os departamentos por nome
            return _context.Department.OrderBy(list =>list.Name).ToList();
        }
    }
}
