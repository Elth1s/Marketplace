namespace WebAPI.Helpers.Localization
{
    public class LocalizationPipeline
    {
        public void Configure(IApplicationBuilder app, RequestLocalizationOptions options)
        {

            app.UseRequestLocalization(options);
        }
    }
}
