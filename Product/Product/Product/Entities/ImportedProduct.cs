using System;
using System.Collections.Generic;

namespace Products.Entities
{
    public class ImportedProduct : Product
    {
        public double CustomsFee { get; set; } 

        public ImportedProduct(){}

        public ImportedProduct(string name, double price, double customsFee)
            :base(name, price)
        {
            CustomsFee = customsFee;
        }

        public double TotalPrice(){
            return Price + CustomsFee;
        }

        public override string PriceTag(){
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Name + " $ " + TotalPrice() + "(Customs fee: $)" + CustomsFee);
            return sb;
        }
    }
}