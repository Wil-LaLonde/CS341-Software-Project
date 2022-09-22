namespace Lab2;


/**
 * Names: Brady Braun & Wil LaLonde
 * Description: Lab 2
 * Date: 9/22/2022
 * Bugs: Not sure yet.
 * Reflection: Did we like this assignment?
 * 
 */
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

		return builder.Build();
	}
}
