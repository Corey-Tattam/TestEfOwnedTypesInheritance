using System;

namespace Domain.Entities
{
    public class Document
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public string DocumentStringFieldOne { get; set; } = string.Empty;

        public string DocumentStringFieldTwo { get; set; } = string.Empty;

        public string DocumentStringFieldThree { get; set; } = string.Empty;

        public DateTime DocumentDateFieldOne { get; set; }

        public DateTime DocumentDateFieldTwo { get; set; }


        public OrderBase Order { get; set; } = null!;
    }
}
