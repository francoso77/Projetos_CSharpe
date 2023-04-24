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
        public List<Department> FindAll()
        {
            //usando o LINQ ordenamos os departamentos por nome - metodo sincrono
            return _context.Department.OrderBy(list => list.Name).ToList();
        
        }
        //fazendo pelo metodo assicrono
        public async Task<List<Department>> FindAllAsync()
        {
            //o await não bloqueia a operação 
            return await _context.Department.OrderBy(list => list.Name).ToListAsync();
        }
    }
}
