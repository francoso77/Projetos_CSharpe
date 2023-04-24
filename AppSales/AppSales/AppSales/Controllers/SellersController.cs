using AppSales.Models;
using AppSales.Models.ViewModels;
using AppSales.Services;
using AppSales.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        /*public IActionResult IndexSincrono()
        {
            //aqui vc pega a função findall criada no servico 
            var list = _sellerServices.FindAll();
            //passa a lista para o view via parametro
            return View(list);
        }*/

        //criando uma index para o Async
        public async Task<IActionResult> Index()
        {
            var list = await _sellerServices.FindAllAsync();
            return View(list);
        }

        //esse metodo create é quem abre o formulário create de Sellers para cadastrar um vendedor
        /*public IActionResult CreateSincrono()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }*/
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        /*//criando o metodo create que para inserir os dados no banco
        //indicar q a ação é de POST e não de GET
        [HttpPost]
        //anotação para prevenção contra ataque CSRF - aproveitando autenticação para enviar vírus
        [ValidateAntiForgeryToken]
        public IActionResult CreateSincrono(Seller seller) 
        {
            //para testar o obj seller se está com os dados corretos antes de enviar para o bd 
            //e para não deixar gravar dados em branco ou faltantes
            
            if(!ModelState.IsValid)
            {
                List<Department> departments = _departmentService.FindAll();
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            _sellerServices.Insert(seller);
            //redireiona a página para a view index do seller
            return RedirectToAction(nameof(Index));
        }*/
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerServices.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }
        /*public IActionResult DeleteSincrono(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided"});
            }
            var seller = _sellerServices.FindById(id.Value); //id.value pq o id do paramentro é um EnuaBlle opcional

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            return View(seller);
        }*/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }
            var seller = await _sellerServices.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            return View(seller);
        }
        /*
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteSincrono (int id)
        {
            _sellerServices.Remove(id);
            return RedirectToAction(nameof(Index));
        }*/
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerServices.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        /*public IActionResult DetailSincrono(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
                //return NotFound();
            }
            var seller = _sellerServices.FindById(id.Value); 

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
                //return NotFound();
            }

            return View(seller);
        }*/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }
            var seller = await _sellerServices.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }
            return View(seller);
        }
        /*public IActionResult EditSincrono(int? id)
        {
            //teste se o id existe e depois se no bd tem esse id
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
                //return NotFound();
            }
            //aqui o id é id.value, pq vc colocou o paramentro como ?
            var seller = _sellerServices.FindById(id.Value);
            
            if(seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" }); 
                //return NotFound();
            }

            //criando a lista de departamentos para a caixa de select
            List<Department> departments = _departmentService.FindAll();
            //criando ao viewmodel com o obj recebido e com a lista criada
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }*/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }
            var seller = await _sellerServices.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }
        /*
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult EditSincrono(int id, Seller seller)
        {
            //para testar o obj seller se está com os dados corretos antes de enviar para o bd 
            //e para não deixar gravar dados em branco ou faltantes

            if (!ModelState.IsValid)
            {
                List<Department> departments = _departmentService.FindAll();
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            //agora testo se o ID existe em seller
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
            }
            try
            {
                //atualizou os dados
                _sellerServices.Update(seller);
                //volta para a view index
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
        }*/
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id mismatch" });
            }
            try
            {
                await _sellerServices.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { 
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            };
            return View(viewModel);
        }
    }
}
