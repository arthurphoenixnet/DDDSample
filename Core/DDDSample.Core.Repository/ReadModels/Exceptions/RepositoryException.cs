using System;

namespace DDDSample.Core.Repository.ReadModels.Exceptions
{
    [Serializable]
    public class RepositoryException : Exception
    {
        #region Constructors

        public RepositoryException()
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception inner) : base(message, inner)
        {
        }

        #endregion Constructors

        protected RepositoryException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}