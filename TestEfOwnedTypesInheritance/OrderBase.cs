using System.Collections.Generic;

namespace TestEfOwnedTypesInheritance
{
    public abstract class OrderBase
    {
        public int Id { get; set; }

        public OrderType OrderType { get; set; }

        public CommonAddress PropertyAddress { get; set; }


        public ICollection<IndividualConsumer> IndividualConsumers { get; set; }

        public ICollection<OrganisationalConsumer> OrganisationalConsumers { get; set; }
    }

    public enum OrderType { SettlementOrder, CaveatOrder }

    public class SettlementOrder : OrderBase
    {
        public ForwardingAddress ForwardingAddress { get; set; }

    }

    public class CaveatOrder : OrderBase
    {

    }

}



