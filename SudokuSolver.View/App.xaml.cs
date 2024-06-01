using System.Windows;
using Autofac;
using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;
using SudokuSolver.Model.Services;
using SudokuSolver.Model.Services.DataParser;
using SudokuSolver.ViewModel;

namespace SudokuSolver.View
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<GamesManager>().As<IGameManager>();
			builder.RegisterType<AppViewModel>();
			builder.RegisterType<SudokuFileManager>().As<ISudokuDataManager>();
			builder.RegisterType<SudokuParserFactory>().As<ISudokuParserFactory>();

			IContainer container = builder.Build();

			AppViewModel sudokuViewModel = container.Resolve<AppViewModel>();

			var mainWindow = new MainWindow
			{
				DataContext = sudokuViewModel
			};
			mainWindow.Show();

			base.OnStartup(e);
		}
	}

}
