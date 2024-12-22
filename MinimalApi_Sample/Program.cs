using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using MinimalApi_Sample.Data;
using MinimalApi_Sample.Models;
using MinimalApi_Sample.Dtos;
using MinimalApi_Sample.Mappers;
using System.Collections;
using FluentValidation;
using MinimalApi_Sample;
using System.Net;

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
    
    APIResponse response = new();
    response.Result = CouponStore.couponList;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;

    return Results.Ok(response);
}).Produces<IEnumerable<APIResponse>>(200);



app.MapGet("/api/coupon/{id:int}", (ILogger<Program> _logger, int id) => {

    APIResponse response = new();
    response.Result = CouponStore.couponList.FirstOrDefault(c => c.Id == id);
    if (response.Result == null)
    {
        response.IsSuccess = false;
        response.StatusCode = HttpStatusCode.NotFound;
        response.ErrorMessage.Add($"Coupon with id {id} does not exist");
        return Results.NotFound(response);
    }
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;

    return Results.Ok(response);
}).WithName("GetCoupon").Produces<APIResponse>(200);



app.MapPost("/api/coupon/", async (IValidator<CreateCouponDto> _validator, [FromBody] CreateCouponDto newCouponDto) => {
    APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


    var validationResult = await _validator.ValidateAsync(newCouponDto);

    if (!validationResult.IsValid)
    {
        response.ErrorMessage.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }
    if (CouponStore.couponList.FirstOrDefault(c => c.Name.ToLower() == newCouponDto.Name.ToLower()) != null)
    {
        response.ErrorMessage.Add("Coupon name already exists");
        return Results.BadRequest(response);
    }

    var createdCoupon = newCouponDto.ToCouponFromCreateDto();

    createdCoupon.Id = CouponStore.couponList.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
    CouponStore.couponList.Add(createdCoupon);
    CouponDto couponDto = createdCoupon.ToCouponDtoFromCoupon();

    response.Result = couponDto;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.Created;
    return Results.Ok(response);

    //return Results.CreateddAtRoute("CreateCoupon", new{ id = createdCoupon.Id}, createdCoupon.ToCouponDtoFromCoupon());
    //return Results.Created($"/api/coupon/{createdCoupon.Id}", createdCoupon);
}).WithName("CreateCoupon").Accepts<CreateCouponDto>("application/json").Produces<APIResponse>(201).Produces(400); 



app.MapPut("/api/coupon/", async (IValidator<UpdateCouponDto> _validator, [FromBody] UpdateCouponDto updateCouponDto) => {
    APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };


    var validationResult = await _validator.ValidateAsync(updateCouponDto);

    if (!validationResult.IsValid)
    {
        response.ErrorMessage.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }

    var existingCoupon = CouponStore.couponList.FirstOrDefault(c => c.Id == updateCouponDto.Id);
    if (existingCoupon == null)
    {
        response.IsSuccess = false;
        response.StatusCode = HttpStatusCode.NotFound;
        response.ErrorMessage.Add($"Coupon with id {updateCouponDto.Id} does not exist");
        return Results.NotFound(response);
    }

    existingCoupon.Name = updateCouponDto.Name;
    existingCoupon.PercentDiscount = updateCouponDto.PercentDiscount;
    existingCoupon.IsActive = updateCouponDto.IsActive;

    response.Result = existingCoupon.ToCouponDtoFromCoupon();
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;
    return Results.Ok(response);

    //return Results.CreateddAtRoute("CreateCoupon", new{ id = createdCoupon.Id}, createdCoupon.ToCouponDtoFromCoupon());
    //return Results.Created($"/api/coupon/{createdCoupon.Id}", createdCoupon);
}).WithName("UpdateCoupon").Accepts<UpdateCouponDto>("application/json").Produces<APIResponse>(200).Produces(400);




app.MapDelete("/api/coupon/{id:int}", (int id) => {
    
});


app.UseHttpsRedirection();

app.Run();
