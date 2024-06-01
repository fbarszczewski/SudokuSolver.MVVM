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

			builder.RegisterType<SudokuGameManager>().As<IGameManager>();
			builder.RegisterType<MainViewModel>();
			builder.RegisterType<SudokuFileManager>().As<ISudokuDataManager>();
			builder.RegisterType<SudokuParserFactory>().As<ISudokuParserFactory>();

			IContainer container = builder.Build();

			MainViewModel sudokuViewModel = container.Resolve<MainViewModel>();

			var mainWindow = new MainWindow
			{
				DataContext = sudokuViewModel
			};
			mainWindow.Show();

			base.OnStartup(e);
		}
	}

}
