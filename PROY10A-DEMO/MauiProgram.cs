using Microsoft.Extensions.Logging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace PROY10A_DEMO;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Inicializar Firebase desde carpeta local "Data"
        try
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "Data", "firebase-adminsdk.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ Archivo de credenciales no encontrado en: {filePath}");
            }
            else
            {
                Console.WriteLine($"📄 Archivo de credenciales encontrado en: {filePath}");

                if (FirebaseApp.DefaultInstance == null)
                {
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential = GoogleCredential.FromFile(filePath)
                    });
                    Console.WriteLine("✅ FirebaseApp inicializado correctamente.");
                }
                else
                {
                    Console.WriteLine("ℹ️ FirebaseApp ya estaba inicializado.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al inicializar Firebase: {ex.Message}");
        }

#if DEBUG
        builder.Logging.AddDebug();
#endif

        Console.WriteLine(FirebaseApp.DefaultInstance != null
            ? "✅ Firebase está listo."
            : "❌ Firebase sigue sin inicializarse.");

        return builder.Build();
    }
}