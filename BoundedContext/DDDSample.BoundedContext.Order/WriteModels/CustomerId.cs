using DDDSample.Core.SharedKernel.Interfaces;
using System;

namespace DDDSample.BoundedContext.Orders.WriteModels
{
    public class CustomerId : IAggregateId
    {
        private const string IdAsStringPrefix = "Cust-";

        #region Constructors

        private CustomerId(Guid id)
        {
            this.Id = id;
        }

        public CustomerId(string id)
        {
            this.Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; private set; }

        #endregion Properties

        #region Public Methods

        public static CustomerId NewCustomerId()
        {
            return new CustomerId(Guid.NewGuid());
        }

        public string IdAsString()
        {
            return $"{IdAsStringPrefix}{this.Id}";
        }

        #endregion Public Methods

        #region Override Methods

        public override string ToString()
        {
            return this.IdAsString();
        }

        public override bool Equals(object obj)
        {
            return obj is CustomerId && Equals(Id, ((CustomerId)obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion Override Methods
    }
}