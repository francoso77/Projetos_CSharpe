//o view model é um model composto para colocar mais dados na tela
namespace AppSales.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public  ICollection<Department> Departments { get; set; }
    }
}
