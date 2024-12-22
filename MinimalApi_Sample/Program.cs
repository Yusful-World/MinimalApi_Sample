using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_Sample.Data;
using MinimalApi_Sample.Models;
using MinimalApi_Sample.Dtos;
using MinimalApi_Sample.Mappers;
using System.Collections;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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


app.MapGet("/api/coupon", (ILogger<Program> _logger) => {
    _logger.Log(LogLevel.Information, "Getting all Coupons");
    return Results.Ok(CouponStore.couponList);
}).Produces<IEnumerable<Coupon>>(200);

app.MapGet("/api/coupon/{id:int}", (ILogger<Program> _logger, int id) => {
    return Results.Ok(CouponStore.couponList.FirstOrDefault(c => c.Id == id));
}).WithName("GetCoupon").Produces<Coupon>(200);

app.MapPost("/api/coupon/", (IValidator<CreateCouponDto> _validator, [FromBody] CreateCouponDto newCoupon) => {
    var validationResult = _validator.ValidateAsync(newCoupon).GetAwaiter().GetResult();

    if (!validationResult.IsValid)
        return Results.BadRequest(validationResult.Errors.FirstOrDefault().ToString());

    if (CouponStore.couponList.FirstOrDefault(c => c.Name.ToLower() == newCoupon.Name.ToLower()) != null)
        return Results.BadRequest("Coupon name already exists");

    var createdCoupon = newCoupon.ToCouponFromCreate();

    createdCoupon.Id = CouponStore.couponList.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
    CouponStore.couponList.Add(createdCoupon);

    return Results.CreatedAtRoute("CreateCoupon", new{ id = createdCoupon.Id}, createdCoupon.ToCouponDtoFromCoupon());
    //return Results.Created($"/api/coupon/{createdCoupon.Id}", createdCoupon);
}).WithName("CreateCoupon").Accepts<CreateCouponDto>("application/json").Produces<CouponDto>(201).Produces(400); 

app.MapPut("/api/coupon/", () => {
    
}); 

app.MapDelete("/api/coupon/{id:int}", (int id) => {
    
});

app.UseHttpsRedirection();

app.Run();
