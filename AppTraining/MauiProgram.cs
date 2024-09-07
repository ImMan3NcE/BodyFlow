using AppTraining.Repositories;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PanCardView;


namespace AppTraining
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseCardsView()
                //.UseAcrylicView()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("DESTROY.ttf", "destroy");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //builder.Services.AddSingleton<ExerciseRepositories>();
            //builder.Services.AddSingleton<WorkoutNoteRepository>();
            builder.Services.AddSingleton<BaseRepository>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
