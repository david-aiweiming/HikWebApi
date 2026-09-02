using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace HikWebApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //new AppSettingModel().Initial(configuration);
            LogHelper.Configure(); //使用前先配置
            //所有数据加载到REDIS
            //if (AppSettingModel.IsLoadSettings == "1")
            //{
            //    LogHelper.LoggerError(typeof(Startup), "所有数据加载到REDIS");
            //    IBaDictionaryCodeDAL baDictionaryCodeDAL = DataAccess.CreateBaDictionaryCodeDAL();
            //    List<BaDictionaryCodeModel> baDictionaryCodeList = baDictionaryCodeDAL.GetList();
            //    RedisHelper redisHelper = new RedisHelper();
            //    if (baDictionaryCodeList != null && baDictionaryCodeList.Count > 0)
            //    {
            //        foreach (var item in baDictionaryCodeList)
            //        {
            //             //防止数据库里面有空数据导致报错，服务不启动
            //            if (string.IsNullOrEmpty(item.stock_code) || string.IsNullOrEmpty(item.platform_code) || string.IsNullOrEmpty(item.cp_code) || string.IsNullOrEmpty(item.dictionary_type))
            //            {
            //                continue;
            //            }
            //            if (string.IsNullOrEmpty(item.owner_code))
            //                item.owner_code = string.Empty;
            //            string key = item.stock_code.Trim() + item.owner_code.Trim() + item.platform_code.Trim() + item.cp_code.Trim() + item.dictionary_type.Trim();
            //            redisHelper.HashSet(key, item.dictionary_code, item.dictionary_value);
            //        }
            //    }
            //}
            //Task.Run(() =>
            //{
            //    Canal.CanalHelper.initConnector();
            //    LogHelper.LoggerError(typeof(Startup), "Canal服务启动");

            //});

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.Configure<AppSettingModel>(Configuration.GetSection("AppSettings"));

            #region 注册服务
            //services.AddScoped<IWaybillApplyRequestService, WaybillApplyRequestService>();
            //services.AddScoped<IBasicDataService, BasicDataService>();
            //services.AddScoped<IWaybillUploadService, WaybillUploadService>();
            //services.AddScoped<IWaybillCancelRequestService, WaybillCancelRequestService>();
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                    builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            IMvcBuilder mvcBuilder = services.AddMvc();
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            // OpenAPI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hik Web Api",
                    Version = "v1",
                    Description = "海康接口平台",
                });
                var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = System.IO.Path.Combine(basePath, "HikWebApi.xml");
                options.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //配置Cors
            app.UseCors("any");

            app.UseAuthentication();
            app.UseAuthorization();

            // 启用Swagger中间件
            app.UseSwagger();
            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HikWebApi");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

