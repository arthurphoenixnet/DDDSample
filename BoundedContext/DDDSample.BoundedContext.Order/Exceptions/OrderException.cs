using System;

namespace DDDSample.BoundedContext.Orders.Exceptions
{
    [Serializable]
    public class OrderException : Exception
    {
        public OrderException()
        {
        }

        public OrderException(string message) : base(message)
        {
        }

        public OrderException(string message, Exception inner) : base(message, inner)
        {
        }

        protected OrderException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}