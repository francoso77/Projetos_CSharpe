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

        public List<Seller> FindAll()
        {
            //aqui neste momento esta função está sincrona - vai rodar e a aplicação fica bloqueada ate terminar
            return _context.Seller.ToList();
        }

        //pelo metodo assincrono
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        public void Insert(Seller seller)
        {
            //metodo para inserir o primeiro departamento no BD
            //forçando um departamento para o cadastro
            //seller.Department = _context.Department.First();

            _context.Add(seller);
            _context.SaveChanges();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }
        public Seller FindById(int id)
        {
            //aqui foi feito o eager loading para carregar tabelas relaciondas
            return _context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.Id == id);
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.
                Include(seller => seller.Department).
                FirstOrDefaultAsync(seller => seller.Id == id);
        }
        public void Remove(int id)
        {
            var seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller); //objeto removido do DBSET
            _context.SaveChanges(); //confirma a remoção com frameEntity no banco de dados
        }
        public async Task RemoveAsync(int id)
        {
            var seller = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(seller);
            await _context.SaveChangesAsync();
        }
        public void Update(Seller seller)
        {
            //antes de atualizar, testo se tem algum obj no bd com o id 
            if (!_context.Seller.Any(sellerBD => sellerBD.Id == seller.Id))
            {
                //se não tem o Id eu mando a menssagem 
                throw new NotFoundException("Id not found!");
            }
            //se tem o Id atualiza os dados
            //porém neste ponto tem q testar se tem alguém usando este id
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                //aqui tratamos a excessão de bd como servico pelo Controller da tabela
                throw new DbConcurrencyException(e.Message);
            }
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
