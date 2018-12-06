using DDDSample.Core.SharedKernel.Interfaces;
using System;

namespace DDDSample.BoundedContext.Orders.WriteModels
{
    public class OrderId : IAggregateId
    {
        private const string IdAsStringPrefix = "Ord-";

        #region Constructors

        private OrderId(Guid id)
        {
            this.Id = id;
        }

        public OrderId(string id)
        {
            this.Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; private set; }

        #endregion Properties

        #region Public Methods

        public static OrderId NewOrderId()
        {
            return new OrderId(Guid.NewGuid());
        }

        public string IdAsString()
        {
            return $"{IdAsStringPrefix}{this.Id.ToString()}";
        }

        #endregion Public Methods

        #region Override Methods

        public override bool Equals(object obj)
        {
            return obj is OrderId && Equals(this.Id, ((OrderId)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.IdAsString();
        }

        #endregion Override Methods
    }
}