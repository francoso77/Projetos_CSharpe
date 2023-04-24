using AppSales.Data;
using AppSales.Models;
using Microsoft.EntityFrameworkCore;

namespace AppSales.Services
{
    public class DepartmentService
    {
        private readonly AppSalesContext _context;
        public DepartmentService(AppSalesContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> FindAllAsync()
        {
            //o await não bloqueia a operação 
            return await _context.Department.OrderBy(list => list.Name).ToListAsync();
        }
    }
}
