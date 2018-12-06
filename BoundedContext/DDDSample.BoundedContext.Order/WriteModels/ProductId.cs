using DDDSample.Core.SharedKernel.Interfaces;
using System;

namespace DDDSample.BoundedContext.Orders.WriteModels
{
    public class ProductId : IAggregateId
    {
        private const string IdAsStringPrefix = "Prd-";

        #region Constructors

        private ProductId(Guid id)
        {
            this.Id = id;
        }

        public ProductId(string id)
        {
            this.Id = Guid.Parse(id.StartsWith(IdAsStringPrefix) ? id.Substring(IdAsStringPrefix.Length) : id);
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; set; }

        #endregion Properties

        #region Public Methods

        public static ProductId NewProductId()
        {
            return new ProductId(Guid.NewGuid());
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
            return obj is ProductId && Equals(this.Id, ((ProductId)obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator !=(ProductId left, ProductId right)
        {
            return !(left == right);
        }

        public static bool operator ==(ProductId left, ProductId right)
        {
            return Equals(left?.Id, right?.Id);
        }

        #endregion Override Methods
    }
}