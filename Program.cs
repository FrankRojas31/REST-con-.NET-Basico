using Microsoft.OpenApi.Models;
using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi Primera API", Description = "Mi primera API con ASP.NET", Version = "v1" });
});
    
var app = builder.Build();
    
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "MI PRIMERA API");
});

// Datos dummy sin conexión a la BD.
var data =new List<string> {  "id", "nombre", "Emmanuel" };

// Haciendo Ruteamiento.
var user = app.MapGroup("/users");

// Mostrar Total de Datos.
user.MapGet("/GetAll", () => data);

//Mostrar datos de uno en especifico.
// user.MapGet("/GetUser/{id}", (int id) => data.SingleOrDefault(user => user.Id == id));

app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));


    
app.Run();