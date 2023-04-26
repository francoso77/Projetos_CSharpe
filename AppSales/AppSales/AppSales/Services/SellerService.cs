using AppSales.Data;
using AppSales.Models;
using AppSales.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AppSales.Services
{
    public class SellerService
    {
        private readonly AppSalesContext _context;

        public SellerService(AppSalesContext context)
        {
            _context = context; 
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller); 
            _context.SaveChanges();
        }
        //.Include() é para mostrar o item relacionado com outra tablea
        public Seller FindById(int id) => _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Seller seller)
        {
            bool hasAny = _context.Seller.Any(x => x.Id == seller.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
