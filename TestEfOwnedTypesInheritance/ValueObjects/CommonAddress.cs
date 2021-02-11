using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestEfOwnedTypesInheritance.ValueObjects
{
    public class CommonAddress : ValueObject
    {

        public CommonAddress(string street, string suburb, string state)
        {
            this.Street = street;
            this.Suburb = suburb;
            this.State = state;
        }

        public string Street { get; private set; }
        public string Suburb { get; private set; }
        public string State { get; private set; }


        public static implicit operator string(CommonAddress address) =>
            address.ToString();

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(Street))
            {
                builder.Append(Street.Trim());
            }

            var secondaryFields = new[] { Suburb, State }
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .ToArray();

            return !secondaryFields.Any()
                ? builder.ToString()
                : builder.Append(builder.Length != 0 ? ", " : string.Empty)
                    .Append(string.Join(" ", secondaryFields))
                    .ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return Suburb;
            yield return State;
        }

    }

    public class ForwardingAddress : CommonAddress
    {
        public ForwardingAddress(string street, string suburb, string state, string country) : base(street, suburb, state)
        {
            this.Country = country;
        }

        public string Country { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return base.GetAtomicValues();
            yield return Country;
        }

    }

    public class PencilClientAddress : CommonAddress
    {
        public PencilClientAddress(string street, string suburb, string state, string someOtherField, string someOtherField2) : base(street, suburb, state)
        {
            this.SomeOtherField = someOtherField;
            this.SomeOtherField2 = someOtherField2;
        }

        public string SomeOtherField { get; }
        public string SomeOtherField2 { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return base.GetAtomicValues();
            yield return SomeOtherField;
            yield return SomeOtherField2;
        }

    }

}



