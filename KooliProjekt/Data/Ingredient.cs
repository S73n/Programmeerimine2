namespace KooliProjekt.Data
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string Unit { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal QuantityUsed { get; set; }
        public Decimal TotalCost { get; set; }
    }
}