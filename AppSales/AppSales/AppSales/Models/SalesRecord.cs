using AppSales.Models.Enums;

namespace AppSales.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SaleStatus  Status { get; set; }
        public Seller Seller { get; set; }  

        //o framework precisa do construtor criado
        public SalesRecord() { }

        //qdo nao te coleções criasse com argumentos para todos os campos
        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
