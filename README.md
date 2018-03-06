# Swashbuckle.AspNetCore.Polymorphism
Polymorhism doc and schema filters

# How to use
Create a new TypesPasser to pass your collection of base types to the filters.
Add Filters to config.
``` 
var types = new TypesPasser(new []{
       typeof(ProjectBase),
       typeof(RegistrationBase)
});
services.AddSwaggerGen(options =>
{
      options.SwaggerDoc("v1", new Info() {Title = "Project Registration Service", Version = "v1"});
      options.DocumentFilter<PolymorphismDocumentFilter>(types);
      options.SchemaFilter<PolymorphismSchemaFilter>(types);
});
            ```
