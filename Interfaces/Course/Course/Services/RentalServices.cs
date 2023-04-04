using System;
using Course.Entities;


namespace Course.Services
{
    public class RentalServices
    {
          public double PricePerHour { get; private set; }      
          public double PricePerDay { get; private set; }

          private BrazilTaxService _brazilTaxService = new BrazilTaxService();

          public RentalServices(double pricePerHour, double pricePerDay)
          {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
          }

          public void ProcessInvoice(CarRental carRental)
          {
               TimeSpan duration =  CarRental.Finish.Substract(CarRental.Start);

               double basicPayment = 0.0;
               if(duration.TotalHours <= 12.0)
               {
                    basicPayment = PricePerHour * Math.Ceiling(duration.TotalHours);
               }
               else
               {
                    basicPayment = PricePerDay * Math.Ceiling(duration.TotalDays);
               }

               double tax = _brazilTaxService.Tax(basicPayment);

               CarRental.Invoice = new Invoice(basicPayment, tax);

          }
    }
}