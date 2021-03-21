using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain.Base
{
    [Serializable]
    public abstract class DomainException : Exception
    {
        protected DomainException()
        {
            
        }

        protected DomainException(string message) : base(message)
        {
            
        }
    }
}
