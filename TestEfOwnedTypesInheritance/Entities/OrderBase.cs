using System.Collections.Generic;
using TestEfOwnedTypesInheritance.ValueObjects;

namespace TestEfOwnedTypesInheritance.Entities
{
    public abstract class OrderBase
    {
        public int Id { get; set; }

        public OrderType OrderType { get; set; }

        public CommonAddress PropertyAddress { get; set; } = null!;

        public string Identifier { get; set; } = string.Empty;


        public ICollection<IndividualConsumer> IndividualConsumers { get; set; } = null!;

        public ICollection<OrganisationalConsumer> OrganisationalConsumers { get; set; } = null!;

        public ICollection<Document> Documents { get; set; } = null!;
    }

    public enum OrderType { SettlementOrder, CaveatOrder }

    public class SettlementOrder : OrderBase
    {
        public ForwardingAddress ForwardingAddress { get; set; } = null!;

    }

    public class CaveatOrder : OrderBase
    {

    }

}



