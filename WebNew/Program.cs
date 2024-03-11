using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebNew.Data;
using Microsoft.Extensions.DependencyInjection;

namespace WebNew
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<WebNewsContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("WebNewsContext") ?? throw new InvalidOperationException("Connection string 'WebNewsContext' not found.")));
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // ������������ ���������� ��������� ������ JWT
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
    //        builder.Services.AddDbContext<WebNew.Data.WebNew>(options =>
    //options.UseNpgsql(builder.Configuration.GetConnectionString("WebNewContext") ?? throw new InvalidOperationException("Connection string 'WebApplicationTestContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles(); // ��������� middleware ��� ������������ ����������� ������ �� ����� wwwroot
            List<string> requestMain = new List<string>();
            //requestMain.Add()

            app.MapWhen(context => context.Request.Path.StartsWithSegments("/"), appBuilder =>
            {
                appBuilder.Run(async (context) =>
                {

                    //����� �����������
                    string add = "";
                    // ��������� ���������� ������� ������� ������ �� C#
                    if (context.Request.Method == "POST" && context.Request.Form.ContainsKey("�����"))
                    {
                        string mail1 = context.Request.Form["mail"];
                        string password = context.Request.Form["password"];

                        if (!string.IsNullOrEmpty(mail1) && !string.IsNullOrEmpty(password))
                        {
                            //User user = new User(0, "", 0, mail1, password);

                            ////User data = work.SelectUser(user);
                            //context.Response.Redirect($"/login/{data.Name}");
                        }


                        //string Reg = context.Request.Form["�����������"];
                    }

                    if (context.Request.Method == "POST" && context.Request.Form.ContainsKey("�����������"))
                    {
                        context.Response.Redirect("/Reg");


                    }



                    if (context.Request.Method == "GET" || context.Request.Method == "POST")
                    {
                        var indexHtmlContent = System.IO.File.ReadAllText("wwwroot/html/index.html");
                        add = indexHtmlContent;
                        context.Response.ContentType = "text/html; charset=utf-8"; // ��������� ���������� ���������
                        await context.Response.WriteAsync(add);
                    }



                });
            });

            app.MapWhen(context => context.Request.Path.StartsWithSegments("/Reg"), appBuilder =>
            {
                appBuilder.Run(async (context) =>
                {
                    string add = "";

                    if (context.Request.Method == "POST" && context.Request.Form.ContainsKey("������������������"))
                    {
                        string name = context.Request.Form["name"];
                        string age = context.Request.Form["age"];
                        string mail = context.Request.Form["mailreg"];
                        string password = context.Request.Form["passwordreg"];
                        //User user = new User(0, name, Convert.ToInt32(age), mail, password);
                        //work.adduser(user);
                        context.Response.Redirect("/", false);
                    }

                    if (context.Request.Method == "POST" && context.Request.Form.ContainsKey("�����"))
                    {
                        context.Response.Redirect("/", false);

                    }

                    if (context.Request.Method == "GET")
                    {
                        var indexHtmlContent = System.IO.File.ReadAllText("wwwroot/html/Reguser.html");
                        add = indexHtmlContent;
                        context.Response.ContentType = "text/html; charset=utf-8"; // ��������� ���������� ���������
                        await context.Response.WriteAsync(add);
                    }
                });
            });
            /// Reg
            app.MapWhen(context => context.Request.Path.StartsWithSegments("/admin"), appBuilder =>
            {
                appBuilder.Run(async (context) =>
                {

                    string add = "";
                    if (context.Request.Method == "POST" && context.Request.Form.ContainsKey("�����User"))
                    {

                        context.Response.Redirect($"/admin/managmentuser/", false);

                    }

                    if (context.Request.Method == "GET" || context.Request.Method == "POST")
                    {
                        var indexHtmlContent = System.IO.File.ReadAllText("wwwroot/html/Admin.html");
                        add = indexHtmlContent;
                        context.Response.ContentType = "text/html; charset=utf-8"; // ��������� ���������� ���������
                        await context.Response.WriteAsync(add);
                    }
                });
            });

            app.MapWhen(context => context.Request.Path.StartsWithSegments($"/admin/managmentuser/"), appBuilder =>
            {
                appBuilder.Run(async (context) =>
                {


                });
            });

            app.Map("/login/{username}", (string username) =>
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
                // ������� JWT-�����
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                return new JwtSecurityTokenHandler().WriteToken(jwt);
            });
            app.Map("/data", () => new { message = "Hello World!" }).RequireAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}action/{=Index}/{id?}");

            //           app.MapControllerRoute(
            //            name: "default",
            //            pattern: "adminroles/{controller=Rolles}/{action=Index}/{id?}");

            app.MapControllerRoute(name: "default", pattern: "adminroles/{controller=UsersAdmin}/{action=Index}/{id?}");

            app.Run();
        }
    }
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // �������� ������
        public const string AUDIENCE = "MyAuthClient"; // ����������� ������
        const string KEY = "mysupersecret_secretkey!123456789012345678901234567890";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
