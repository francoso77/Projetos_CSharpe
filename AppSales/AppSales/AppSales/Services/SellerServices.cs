using AppSales.Data;
using AppSales.Models;

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

        public void Insert(Seller seller)
        {
            //metodo para inserir os dados no banco de dados
            //forncando um departamento para o cadastro

            //seller.Department = _context.Department.First();
            
            _context.Add(seller);
            _context.SaveChanges();
        }
    }
}
