using EDT.LIbrary.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtensions
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContextFactory<LibraryContext>(options =>
		{
			options.UseSqlite(configuration.GetConnectionString("LibraryContext"));
		});

		return services;
	}
}
