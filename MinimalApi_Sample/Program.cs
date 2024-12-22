using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_Sample.Data;
using MinimalApi_Sample.Models;
using MinimalApi_Sample.Dtos;
using MinimalApi_Sample.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/HelloWorld/{id:int}", (int id) =>
{
    return Results.Ok("int!!" + id);
});
app.MapPost("/HelloWorld1", () => {
return Results.Ok("Hello World! I'm Abdullahi Yusuf. Trying out MapPost");
});


app.MapGet("/api/coupon", () => {
    return Results.Ok(CouponStore.couponList);
});

app.MapGet("/api/coupon/{id:int}", (int id) => {
    return Results.Ok(CouponStore.couponList.FirstOrDefault(c => c.Id == id));
});

app.MapPost("/api/coupon/", ([FromBody] CreateCouponDto newCoupon) => {
    if (string.IsNullOrWhiteSpace(newCoupon.Name))
        return Results.BadRequest("Invalid coupon Id/Name");

    if (CouponStore.couponList.FirstOrDefault(c => c.Name.ToLower() == newCoupon.Name.ToLower()) != null)
        return Results.BadRequest("Coupon name already exists");

    var createdCoupon = newCoupon.ToCouponFromCreate();

    createdCoupon.Id = CouponStore.couponList.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
    CouponStore.couponList.Add(createdCoupon);

    return Results.Ok(createdCoupon);
}); 

app.MapPut("/api/coupon/", () => {
    
}); 

app.MapDelete("/api/coupon/{id:int}", (int id) => {
    
});

app.UseHttpsRedirection();

app.Run();
