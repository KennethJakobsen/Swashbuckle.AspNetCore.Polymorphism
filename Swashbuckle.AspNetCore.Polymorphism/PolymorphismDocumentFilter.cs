using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.Polymorphism
{
    internal class PolymorphismDocumentFilter : IDocumentFilter
    {
        private readonly Type[] _types;

        public PolymorphismDocumentFilter(TypesPasser types)
        {
            _types = types.Types;
        }

        private static void RegisterSubClasses(ISchemaRegistry schemaRegistry, Type abstractType)
        {
            const string discriminatorName = "discriminator";

            var parentSchema = schemaRegistry.Definitions[abstractType.FriendlyId()];

            //set up a discriminator property (it must be required)
            parentSchema.Discriminator = discriminatorName;
            parentSchema.Required = new List<string> { discriminatorName };

            if (!parentSchema.Properties.ContainsKey(discriminatorName))
                parentSchema.Properties.Add(discriminatorName, new Schema { Type = "string" });

            //register all subclasses
            var derivedTypes = abstractType.Assembly
                                           .GetTypes()
                                           .Where(x => abstractType != x && abstractType.IsAssignableFrom(x));

            foreach (var item in derivedTypes)
                schemaRegistry.GetOrRegister(item);
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var type in _types)
            {
                RegisterSubClasses(context.SchemaRegistry, type);
            }
        }
    }
}
