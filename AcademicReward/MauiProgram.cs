using CommunityToolkit.Maui;

namespace AcademicReward;

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
				fonts.AddFont("Nunito-Black.ttf", "BoldFont");
				fonts.AddFont("Nunito-Medium.ttf", "PrimaryFont");
				fonts.AddFont("Nunito-Regular.ttf", "SecondaryFont");
			});
        builder.UseMauiCommunityToolkit();
        return builder.Build();
	}
}
