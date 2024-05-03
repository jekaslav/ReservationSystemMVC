using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using ReservationSystemMVC.Domain.Contexts;
using ReservationSystemMVC.Services.Interfaces;
using ReservationSystemMVC.Services.Mappers;
using ReservationSystemMVC.Services.Services;

namespace ReservationSystemMVC.Web;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
        
        var connection = builder.Configuration.GetConnectionString("PostgresConnection");
        builder.Services.AddDbContext<ReservationSystemDbContext>(opt =>
        {
            opt.UseNpgsql(connection, x => x.MigrationsAssembly("ReservationSystemMVC.Web"));
        });
        
        builder.AddServices();
    }
    
    public static void SetupPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.MapControllerRoute(
            name: "home",
            pattern: "",
            defaults: new { controller = "Home", action = "Index" }
        );

        app.MapControllerRoute(
            name: "classrooms",
            pattern: "classrooms",
            defaults: new { controller = "Classroom", action = "GetAllClassrooms" }
        );
        
        app.MapControllerRoute(
            name: "GetClassroomById",
            pattern: "classrooms/{id:int}",
            defaults: new { controller = "Classroom", action = "GetClassroomById" }
        );
        
        app.MapControllerRoute(
            name: "CreateClassroom",
            pattern: "classroom/create",
            defaults: new { controller = "Classroom", action = "Create" }
        );
        
        app.MapControllerRoute(
            name: "UpdateClassroom",
            pattern: "classrooms/update/{id}",
            defaults: new { controller = "Classroom", action = "Update" }
        );
        
        app.MapControllerRoute(
            name: "DeleteClassroom",
            pattern: "classrooms/delete/{id}",
            defaults: new { controller = "Classroom", action = "Delete" }
        );

        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }


    private static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));

        builder.Services.AddScoped<IStudentService, StudentService>();
        builder.Services.AddScoped<IChiefService, ChiefService>();
        builder.Services.AddScoped<IClassroomService, ClassroomService>();
        builder.Services.AddScoped<IReservationRequestService, ReservationRequestService>();
        
    }
}