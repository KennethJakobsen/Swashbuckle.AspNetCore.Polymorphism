using System;
using System.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.Polymorphism.Extensions
{
    public static class SwaggerGeneratorOptionsExtensions
    {
        public static void AddTypes(this SwaggerGenOptions self, Type[] typesToRegister)
        {
            if(typesToRegister == null) throw new ArgumentNullException(nameof(typesToRegister));
            if(typesToRegister.Any() == false) throw new ArgumentException("types cannot be empty", nameof(typesToRegister));
            
            var parser = new TypesPasser(typesToRegister);
            self.DocumentFilter<PolymorphismDocumentFilter>(parser);
            self.SchemaFilter<PolymorphismSchemaFilter>(parser);
        }
    }
}