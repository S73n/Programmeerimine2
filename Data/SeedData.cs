using KooliProjekt.Data;
using System;
using System.Linq;

namespace KooliProjekt.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            if (context.Beers.Any() || context.Customers.Any() || context.Invoices.Any())
            {
                return;
            }

            var beers = new[]
            {
                new Beer { BeerName = "IPA", BeerDescription = "A hoppy and bitter IPA with citrus notes." },
                new Beer { BeerName = "Pale Ale", BeerDescription = "A refreshing pale ale with mild hops and malt." },
                new Beer { BeerName = "Stout", BeerDescription = "A rich and dark stout with coffee and chocolate notes." },
                new Beer { BeerName = "Lager", BeerDescription = "A crisp and refreshing lager, perfect for any occasion." },
                new Beer { BeerName = "Wheat Beer", BeerDescription = "A cloudy, fruity wheat beer with a light body." }
            };

            context.Beers.AddRange(beers);

            var customers = new[]
            {
                new Customer { Name = "John Doe" },
                new Customer { Name = "Jane Smith" },
                new Customer { Name = "Alice Johnson" },
                new Customer { Name = "Bob Brown" }
            };

            context.Customers.AddRange(customers);

            var invoices = new[]
            {
                new Invoice
                {
                    InvoiceNo = "INV001",
                    InvoiceDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(30),
                    CustomerId = 1
                },
                new Invoice
                {
                    InvoiceNo = "INV002",
                    InvoiceDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(30),
                    CustomerId = 2
                },
                new Invoice
                {
                    InvoiceNo = "INV003",
                    InvoiceDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(30),
                    CustomerId = 3
                }
            };

            context.Invoices.AddRange(invoices);

            var invoiceLines = new[]
            {
                new InvoiceLine { Quantity = 3, Price = 3.99m, BeerId = 1 },
                new InvoiceLine { Quantity = 2, Price = 2.99m, BeerId = 2 },
                new InvoiceLine { Quantity = 2, Price = 4.49m, BeerId = 3 },
                new InvoiceLine { Quantity = 1, Price = 2.49m, BeerId = 4 },
                new InvoiceLine { Quantity = 4, Price = 3.49m, BeerId = 5 }
            };

            context.InvoiceLines.AddRange(invoiceLines);

            context.SaveChanges();
        }
    }
}
