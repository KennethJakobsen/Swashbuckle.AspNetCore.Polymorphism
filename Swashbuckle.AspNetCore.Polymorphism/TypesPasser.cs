using System;

namespace Swashbuckle.AspNetCore.Polymorphism
{
    internal class TypesPasser
    {
        internal TypesPasser(Type[] types)
        {
            Types = types;
        }

        public Type[] Types { get; }
    }
}