namespace TestEfOwnedTypesInheritance
{
    public abstract class ConsumerBase
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public CommonAddress Address { get; set; }


        public OrderBase Order { get; set; }

    }

    public class IndividualConsumer : ConsumerBase
    {
        public string IndividualName { get; set; }

    }

    public class OrganisationalConsumer : ConsumerBase
    {
        public string OrganisationName { get; set; }
    }

}
