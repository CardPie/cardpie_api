using Microsoft.Extensions.Configuration;
using System;

namespace AppCore.Extensions
{
    public static class EnvironmentExtension
    {
        private static IConfiguration Configuration { get; }

        static EnvironmentExtension()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = configurationBuilder.Build();
        }

        // ==================================== GLOBAL ======================================

        // ================ CONTAINS SOME STUPID SECURITY HANDMADE METHOD  ==================
        public static string GetAppLogFolder() =>
            Configuration["AppSettings:LOG_FOLDER"] ?? "Logs";

        public static string GetEnvironment() =>
            Configuration["AppSettings:ASPNETCORE_ENVIRONMENT"] ?? "Development";

        public static bool IsSendOtp() => Configuration.GetValue<bool>("AppSettings:IS_SEND_OTP");
        public static bool IsProduction() => GetEnvironment() == "Production";
        public static bool IsStaging() => GetEnvironment() == "Staging";
        public static bool IsDevelopment() => GetEnvironment() == "Development";

        public static string GetAppConnectionString()
        {
            var hashedConnectionString = Configuration["AppSettings:CONNECTION_STRING"] ?? string.Empty;
            return hashedConnectionString;
            //return EncryptDecrypt.Decrypt(hashedConnectionString);
        }

        public static string GetPath() =>
            Configuration["AppSettings:DOMAIN_PATH"] ?? string.Empty;

        public static string GetJwtIssuer() =>
            Configuration["AppSettings:JWT_ISSUER"] ?? string.Empty;

        public static string GetJwtAudience() =>
            Configuration["AppSettings:JWT_AUDIENCE"] ?? string.Empty;

        public static string GetJwtAccessTokenSecret()
        {
            var hashedJwtToken = Configuration["AppSettings:JWT_ACCESS_TOKEN_SECRET"] ?? string.Empty;

            return EncryptDecrypt.Decrypt(hashedJwtToken);
        }

        public static double GetJwtAccessTokenExpires() =>
            Convert.ToDouble(Configuration["AppSettings:JWT_ACCESS_TOKEN_EXPIRES"] ?? "0");

        public static string GetJwtResetTokenSecret()
        {
            var hashedJwtResetToken= Configuration["AppSettings:JWT_RESET_TOKEN_SECRET"] ?? string.Empty;

            return EncryptDecrypt.Decrypt(hashedJwtResetToken);
        }

        public static double GetJwtResetTokenExpires() =>
            Convert.ToDouble(Configuration["AppSettings:JWT_RESET_TOKEN_EXPIRES"] ?? "0");

        public static string GetGoogleClientId()
        {
            var hashedGoogleClientId = Configuration["AppSettings:GOOGLE_CLIENT_ID"] ?? string.Empty;

            return EncryptDecrypt.Decrypt(hashedGoogleClientId);
        }

        public static string GetGoogleClientSecret()
        {
            var hashedGoogleClientSecret = Configuration["AppSettings:GOOGLE_CLIENT_SECRET"] ?? string.Empty;

            return EncryptDecrypt.Decrypt(hashedGoogleClientSecret);
        }
    }
}
