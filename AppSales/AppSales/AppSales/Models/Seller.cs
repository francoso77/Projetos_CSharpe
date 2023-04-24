using System.ComponentModel.DataAnnotations;

namespace AppSales.Models
{
    public class Seller
    {
        public int Id { get; set; }
        //colocando a {0} ele vai pegar o nome do campo
        [Required(ErrorMessage = "{0} Required"), StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size sould be between {2} and {1}")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} Required")]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        public string Email { get; set; }
        [Display(Name= "Birth Date"), DataType(DataType.Date), DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} Required")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Base Salary"), DisplayFormat(DataFormatString = "{0:N}")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        //este campo é para controle da chave estrangeiro no banco 
        //e não deixar criar um dado com campo null
        public int DepartmentId { get; set; } //aqui vc força o departamento existir pq o campo int não pode ser null
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();  
        
        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        //Amount é o campo valor em SalesRecord
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales
                .Where(sales => sales.Date >= initial && sales.Date <= final)
                .Sum(sales => sales.Amount);
        }

    }
}
