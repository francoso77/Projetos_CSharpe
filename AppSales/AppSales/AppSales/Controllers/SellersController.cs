using AppSales.Models;
using AppSales.Models.ViewModels;
using AppSales.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSales.Controllers
{
    public class SellersController : Controller
    {
        //criando a dependencia do servico

        private readonly SellerServices _sellerServices;
        //atribuindo injetando a dependencia no construtor

        private readonly DepartmentService _departmentService;
        public SellersController(SellerServices sellerServices, DepartmentService departmentService)
        {
            _sellerServices = sellerServices;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            //aqui vc pega a função findall criada no servico 
            var list = _sellerServices.FindAll();
            //passa a lista para o view via parametro
            return View(list);
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments};
            return View(viewModel);
        }
        //criando o metodo create que para inserir os dados no banco
        //indicar q a ação é de POST e não de GET
        [HttpPost]
        //anotação para prevenção contra ataque CSRF - aproveitando autenticação para enviar vírus
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) 
        {
            _sellerServices.Insert(seller);
            //redireiona a página para a view index
            return RedirectToAction(nameof(Index));
        }
    }
}
