using Topshelf;
using Topshelf.Configuration.Dsl;

namespace RinsedPlaylistBlocker
{
	public class Program
	{
		static void Main(string[] args)
		{
			var configuration = RunnerConfigurator.New(runnerConfigurator =>
			{
				runnerConfigurator.ConfigureService<Service>(s =>
				{
					s.HowToBuildService(name => new Service());
					s.WhenStarted(service => service.Start());
					s.WhenStopped(service => service.Stop());
				});

				runnerConfigurator.RunAsLocalSystem();
				runnerConfigurator.DoNotStartAutomatically();

				runnerConfigurator.SetDescription("Death to Belle & Sebastien!");
				runnerConfigurator.SetDisplayName("Rinsed Playlist Blocker");
				runnerConfigurator.SetServiceName("RinsedPlaylistBlocker");
			});
			Runner.Host(configuration, args);
		}
	}
}
