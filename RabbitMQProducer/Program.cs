using RabbitMQ;
using RabbitMQData.Dto;
using RabbitMQData.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("AddUserToQueque", (UserDto userDto) =>
{
    User user = new User()
    {
        Id = userDto.Id,
        CreatedDate = DateTime.UtcNow,
        Name = userDto.Name,
        Password = userDto.Password
    };
    SendQueue.SendToQueue(user);
});

app.Run();