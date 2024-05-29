using System.Windows;
using Autofac;
using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;
using SudokuSolver.Model.Services;
using SudokuSolver.Model.Services.ParseFactory;
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

			builder.RegisterType<AppModel>().As<IAppModel>();
			builder.RegisterType<SudokuViewModel>();
			builder.RegisterType<SudokuDataManager>().As<ISudokuDataManager>();
			builder.RegisterType<SudokuParserFactory>().As<ISudokuParserFactory>();

			IContainer container = builder.Build();

			SudokuViewModel sudokuViewModel = container.Resolve<SudokuViewModel>();

			var mainWindow = new MainWindow
			{
				DataContext = sudokuViewModel
			};
			mainWindow.Show();

			base.OnStartup(e);
		}
	}

}
