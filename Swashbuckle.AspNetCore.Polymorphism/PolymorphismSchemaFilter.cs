using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.Polymorphism
{
    public class PolymorphismSchemaFilter : ISchemaFilter
    {
        private readonly Type[] _types;

        public PolymorphismSchemaFilter(TypesPasser types)
        {
            _types = types.Types;
            _derivedTypes = new Lazy<HashSet<Type>>(Init);
        }

        private readonly Lazy<HashSet<Type>> _derivedTypes;

        private HashSet<Type> Init()
        {
            var result = new HashSet<Type>();

            foreach (var type in _types)
            {
                var abstractType = type;
                var dTypes = abstractType.Assembly
                    .GetTypes()
                    .Where(x => abstractType != x && abstractType.IsAssignableFrom(x));

                foreach (var item in dTypes)
                    result.Add(item);
            }
            
            return result;
        }

        public void Apply(Schema model, SchemaFilterContext context)
        {
            if (!_derivedTypes.Value.Contains(context.SystemType)) return;
            ApplyFilter(model, context.SystemType.BaseType);
        }

        private void ApplyFilter(Schema model, Type parent)
        {
            var clonedSchema = new Schema
            {
                Properties = model.Properties,
                Type = model.Type,
                Required = model.Required
            };

            //schemaRegistry.Definitions[typeof(T).Name]; does not work correctly in SwashBuckle
            var parentSchema = new Schema { Ref = "#/definitions/" + parent.Name };

            model.AllOf = new List<Schema> { parentSchema, clonedSchema };

            //reset properties for they are included in allOf, should be null but code does not handle it
            model.Properties = new Dictionary<string, Schema>();
        }
    }
}