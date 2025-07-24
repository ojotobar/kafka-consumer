using Confluent.Kafka;
using KafkaAndQueueConsumer.BackgroundServices;
using KafkaAndQueueConsumer.Service;

var builder = WebApplication.CreateBuilder(args);
var configs = builder.Configuration;

var kafkaConfig = new ConsumerConfig
{
    BootstrapServers = configs["Kafka:Server"],
    GroupId = configs["Kafka:GroupId"],
    AutoOffsetReset = AutoOffsetReset.Earliest
};

builder.Services.AddSingleton(sp =>
{
    return new ConsumerBuilder<Null, string>(kafkaConfig)
    .SetErrorHandler((_, e) => Console.WriteLine($"Consumer Error: {e.Reason}"))
    .Build();
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IKafkaConsumer, KafkaConsumer>();
builder.Services.AddSingleton<IKafkaMessageHandler,  KafkaMessageHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<KafkaPersonWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
