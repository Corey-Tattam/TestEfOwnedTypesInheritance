using TestEfOwnedTypesInheritance.ValueObjects;

namespace TestEfOwnedTypesInheritance.Entities
{
    public abstract class ConsumerBase
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public CommonAddress Address { get; set; } = null!;


        public OrderBase Order { get; set; } = null!;

    }

    public class IndividualConsumer : ConsumerBase
    {
        public string IndividualName { get; set; } = string.Empty;

    }

    public enum CompanyType
    {
        Company = 1,
        SoleTrader = 2,
        Trust = 3,
        Partnership = 4
    }

    public class OrganisationalConsumer : ConsumerBase
    {
        public string OrganisationName { get; set; } = string.Empty;

        public CompanyType CompanyType { get; set; }
    }

}
