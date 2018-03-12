# Swashbuckle.AspNetCore.Polymorphism
Polymorhism doc and schema filters

# How to use
Use the AddTypes extension to add an array of your base types.
``` 

services.AddSwaggerGen(options =>
{
      options.SwaggerDoc("v1", new Info() {Title = "Project Registration Service", Version = "v1"});
      options.AddTypes(new [] 
      {
           typeof(ProjectBase), 
           typeof(RegistrationBase)
      });
});
