using BlazorBlog.Components;

using BlazorBlog.Services;
using BlazorBlog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddMemoryCache();

// 配置 PostgreSQL 数据库
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services with interfaces
builder.Services.AddScoped<BlazorBlog.Services.Interfaces.IBlogService, BlogService>();
builder.Services.AddScoped<UIService>();
builder.Services.AddScoped<BlazorBlog.Services.Interfaces.IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Register new service layer
builder.Services.AddScoped<BlazorBlog.Services.Interfaces.IPostService, PostService>();
builder.Services.AddScoped<BlazorBlog.Services.Interfaces.ICategoryService, CategoryService>();
builder.Services.AddScoped<BlazorBlog.Services.Interfaces.ITagService, TagService>();
builder.Services.AddScoped<BlazorBlog.Services.Interfaces.ICommentService, CommentService>();
builder.Services.AddScoped<BlazorBlog.Services.Interfaces.IUserService, UserService>();
builder.Services.AddScoped<BlazorBlog.Services.Interfaces.IMarkdownService, MarkdownService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
