using System;

namespace Swashbuckle.AspNetCore.Polymorphism
{
    public class TypesPasser
    {
        public TypesPasser(Type[] types)
        {
            Types = types;
        }

        public Type[] Types { get; }
    }
}