//namespace Car_App
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {

//        }

//        public IConfiguration Configuration { get; }    

//        public void ConfigureServices(IServiceCollection services) { }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                app.UseSwagger();
//                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car_App v1"));
//            }
//            app.UseHttpsRedirection();
//            app.UseRouting();
//            app.UseAuthorization();
//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//        }
//    }
//}
