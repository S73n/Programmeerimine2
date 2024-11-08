namespace KooliProjekt.Data
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int BeerId { get; set; }
        public Beer Beer { get; set; }
    }
}
