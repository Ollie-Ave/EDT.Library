namespace Microsoft.Extensions.DependencyInjection;

using EDT.Library.Services;
using EDT.Library.Services.Interfaces;

public static class IServiceCollectionExtensions
{
	public static IServiceCollection AddLibraryServices(this IServiceCollection services)
	{
		services.AddScoped<IBookService, BookService>();

		return services;
	}
}
