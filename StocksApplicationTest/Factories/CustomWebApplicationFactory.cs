using Application.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace StocksApplicationTest.Factories
{
	public class CustomWebApplicationFactory : WebApplicationFactory<Program>
	{
		public Mock<IStocksService> StocksServiceMock { get; private set; }

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.UseEnvironment("Test");

			builder.ConfigureServices(services =>
			{
				var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MsSqlServerDbContext>));
				if (descriptor != null)
				{
					services.Remove(descriptor);
				}

				services.AddDbContext<MsSqlServerDbContext>(options =>
				{
					options.UseInMemoryDatabase("DatabaseForTesting");
				});

				var stocksServiceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IStocksService));
				if (stocksServiceDescriptor != null)
				{
					services.Remove(stocksServiceDescriptor);
				}

				StocksServiceMock = new Mock<IStocksService>();
				services.AddSingleton(StocksServiceMock.Object);
			});

			base.ConfigureWebHost(builder);
		}
	}
}
