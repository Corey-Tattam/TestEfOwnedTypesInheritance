using Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
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

        public string SettlementStringFieldOne { get; set; } = string.Empty;

        public string SettlementStringFieldTwo { get; set; } = string.Empty;

        public string SettlementStringFieldThree { get; set; } = string.Empty;

        public DateTime SettlementDateFieldOne { get; set; }

        public DateTime SettlementDateFieldTwo { get; set; }

    }

    public class CaveatOrder : OrderBase
    {
        public string CaveatStringFieldOne { get; set; } = string.Empty;

        public string CaveatStringFieldTwo { get; set; } = string.Empty;

        public string CaveatStringFieldThree { get; set; } = string.Empty;

        public DateTime CaveatDateFieldOne { get; set; }

        public DateTime CaveatDateFieldTwo { get; set; }
    }

}



