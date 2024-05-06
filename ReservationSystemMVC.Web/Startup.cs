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
            pattern: "classrooms/{id}",
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
        
        app.MapControllerRoute(
            name: "student",
            pattern: "student",
            defaults: new { controller = "Student", action = "GetAllStudents" }
        );
        
        app.MapControllerRoute(
            name: "GetStudentById",
            pattern: "students/{id}",
            defaults: new { controller = "Student", action = "GetStudentById" }
        );
        
        app.MapControllerRoute(
            name: "CreateStudent",
            pattern: "student/create",
            defaults: new { controller = "Student", action = "Create" }
        );
        
        app.MapControllerRoute(
            name: "UpdateStudent",
            pattern: "student/update/{id}",
            defaults: new { controller = "Student", action = "Update" }
        );
        
        app.MapControllerRoute(
            name: "DeleteStudent",
            pattern: "student/delete/{id}",
            defaults: new { controller = "Student", action = "Delete" }
        );
        
        app.MapControllerRoute(
            name: "chiefs",
            pattern: "chiefs",
            defaults: new { controller = "Chief", action = "GetAllChiefs" }
        );
        
        app.MapControllerRoute(
            name: "GetChiefById",
            pattern: "chiefs/{id}",
            defaults: new { controller = "Chief", action = "GetChiefById" }
        );
        
        app.MapControllerRoute(
            name: "CreateStudent",
            pattern: "chiefs/create",
            defaults: new { controller = "Chief", action = "Create" }
        );
        
        app.MapControllerRoute(
            name: "UpdateStudent",
            pattern: "chiefs/update/{id}",
            defaults: new { controller = "Chief", action = "Update" }
        );
        
        app.MapControllerRoute(
            name: "DeleteStudent",
            pattern: "chiefs/delete/{id}",
            defaults: new { controller = "Chief", action = "Delete" }
        );
        
        app.MapControllerRoute(
            name: "TakeControl",
            pattern: "chief/takecontrol",
            defaults: new { controller = "Chief", action = "TakeControl" }
        );

        app.MapControllerRoute(
            name: "ReleaseControl",
            pattern: "chief/releasecontrol",
            defaults: new { controller = "Chief", action = "ReleaseControl" }
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