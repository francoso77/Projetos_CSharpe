using AppSales.Data;
using AppSales.Models;
using AppSales.Services.Exceptions;
using Microsoft.EntityFrameworkCore;  // para usar a função INCLUDE e fazer JOIN 

namespace AppSales.Services
{
    public class SellerServices
    {
        //criando a dependencia com o context
        private readonly AppSalesContext _context;

        //gerando o construtor do servico passando o context
        public SellerServices(AppSalesContext context)
        {
            _context = context;
        }
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            var seller = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(seller);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(sellerBD => sellerBD.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found!");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
