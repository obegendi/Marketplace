using System;
using System.Diagnostics.CodeAnalysis;

namespace Marketplace.Domain.Seed
{
    public abstract class TypeIdValueBase : IEquatable<TypeIdValueBase>
    {
        public TypeIdValueBase(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public bool Equals([AllowNull] TypeIdValueBase other)
        {
            return Value == other.Value;
        }
    }
}
