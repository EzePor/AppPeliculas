using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Firebase.Auth;

using Firebase.Auth.Providers;

namespace AppPeliculas;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		    builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
		    {
			ApiKey = "AIzaSyAJu02w6o9rVt0aau1BjkQMsWyh_sgnRa4",
			AuthDomain = "fir-autenticacion-dc174.firebaseapp.com",
			Providers = new FirebaseAuthProvider[]
			   {
				new EmailProvider(),
			    }
		    }));


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
