using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.SolvingAlgorithms;

namespace SudokuSolver.Model.Services
{
	internal class SudokuSolverManager
	{
		private readonly List<ISudokuSolver> _solvers;

		public SudokuSolverManager()
		{
			_solvers = new List<ISudokuSolver>
			{
				new SimpleBacktrackingSolver(),
				new BacktrackingSolver(),
				new ConstraintProgrammingSolver(),
				new XwingSolver(),
				new SwordfishSolver()
				// add more solvers here
			};
		}

		public bool Solve(ref byte[,] board,ref string firstAlgorithm)
		{
			if(!SudokuValidator.IsValidBoard(board))
			{
				throw new ArgumentException("Invalid Sudoku _board");
			}

			// try to solve the _board with the first algorithm
			foreach(ISudokuSolver solver in _solvers)
			{
				if(solver.GetName() == firstAlgorithm)
				{
					if(solver.Solve(ref board))
					{
						firstAlgorithm = solver.GetName();
						return true;
					}
					break;
				}
			}

			// try to solve the _board with the other algorithms
			foreach(ISudokuSolver solver in _solvers)
			{
				if(solver.GetName() != firstAlgorithm)
				{
					if(solver.Solve(ref board))
					{
						firstAlgorithm = solver.GetName();
						return true;
					}
					break;
				}
			}

			return false;
		}
		public List<string> GetSolverNames()
		{
			return _solvers.Select(solver => solver.GetName()).ToList();
		}
	}
}
