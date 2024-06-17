using LojaCamisa.Cookie;
using LojaCamisa.GerenciadorArquivos;
using LojaCamisa.Libraries.Login;
using LojaCamisa.Models;
using LojaCamisa.Repository.Interface;
using LojaCamisa.Repository.Interface.Contract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IFormaPagamentoRepository, FormaPagamentoRepository>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(900);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<GerenciadorArquivo>();
builder.Services.AddScoped<Cookie>();
builder.Services.AddScoped<Carrinho>();
builder.Services.AddScoped<LojaCamisa.Libraries.Sessao.Sessao>();
builder.Services.AddScoped<LoginUsuario>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseCookiePolicy();
app.UseSession();


app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(
        name:"areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoint.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
