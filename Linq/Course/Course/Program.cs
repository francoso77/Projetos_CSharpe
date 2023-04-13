using Course.Entities;


namespace Course
{
    class Program
    {

        static void Print<T>(string menssage, IEnumerable<T> collection) 
        {
            Console.WriteLine(menssage);
            foreach (T item in collection)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>()
            {
                new Product(){ Id = 1, Name ="Computer", Price=1100.0, Category = c2 },
                new Product(){ Id = 2, Name ="Hammer", Price=90.0, Category = c1 },
                new Product(){ Id = 3, Name ="TV", Price=1700.0, Category = c3 },
                new Product(){ Id = 4, Name ="Notebook", Price=1300.0, Category = c2 },
                new Product(){ Id = 5, Name ="Saw", Price=80.0, Category = c1 },
                new Product(){ Id = 6, Name ="Tablet", Price=700.0, Category = c2 },
                new Product(){ Id = 7, Name ="Camera", Price=700.0, Category = c3 },
                new Product(){ Id = 8, Name ="Printer", Price=350.0, Category = c3 },
                new Product(){ Id = 9, Name ="MacBook", Price=1800.0, Category = c2 },
                new Product(){ Id = 10, Name ="Sound Bar", Price=700.0, Category = c3 },
                new Product(){ Id = 11, Name ="Level", Price=70.0, Category = c1 }

            };

            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.0);
            Print("Produtos do tipo 1 e preços menor que 900.00: ", r1 );
        }
    }
}