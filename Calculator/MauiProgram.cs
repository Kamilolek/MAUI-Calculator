using Calculator.Controllers;
using Calculator.Views;
using Microsoft.Extensions.Logging;

namespace Calculator;

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
                fonts.AddFont("IBMPlexMono-Regular.ttf", "IBMPlexMonoRegular");
            });
        builder.Services.AddSingleton<EquationController>();
        builder.Services.AddSingleton<CalculatorPage>();
        builder.Services.AddSingleton<HistoryController>();
        builder.Services.AddSingleton<HistoryPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
