namespace TestEfOwnedTypesInheritance.Entities
{
    public class Document
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public OrderBase Order { get; set; } = null!;
    }
}
