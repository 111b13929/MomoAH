using MomoAH.DataAccess;
using MomoAH.Interfaces;
using MomoAH.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ���U DbContext
builder.Services.AddSingleton<DbContext>();

// ���U�Ҧ� Repository
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
builder.Services.AddScoped<IDoctorSpecialtyRepository, DoctorSpecialtyRepository>();
builder.Services.AddScoped<IBusinessHoursRepository, BusinessHoursRepository>();
builder.Services.AddSingleton<DbContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ���U Controllers �M Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ���U CORS �䴩�A���\�Ҧ��ӷ��ШD (��K�e�ݶ}�o�ɨϥ�)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAllOrigins"); // �ҥ� CORS
// �t�m HTTP �޹D
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// �ҥ� CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
