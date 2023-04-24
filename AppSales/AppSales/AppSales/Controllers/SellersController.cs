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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerServices.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerServices.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
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
