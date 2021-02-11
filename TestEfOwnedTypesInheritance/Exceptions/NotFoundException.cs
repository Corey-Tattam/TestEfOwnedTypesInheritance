namespace TestEfOwnedTypesInheritance.Exceptions
{
    public class NotFoundException : UserExceptionBase
    {
        public NotFoundException(string name, object key) : base($"Entity {name} ({key}) was not found.") { }
    }
}
