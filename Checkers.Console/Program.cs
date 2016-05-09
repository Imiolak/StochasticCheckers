using System.Collections.Generic;
using Checkers.Algorithms;
using Checkers.Algorithms.MTCS.Strategy;
using Checkers.Engine.Display;
using Checkers.Experiment;
using Checkers.Experiment.Measurement;

namespace Checkers.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var player1 = new MTCSPlayer(new StaticBudgetAssignStrategy(30), new RandomChildSelectionStrategy(), new WinPercentageChildSelectionStrategy());
            var player2 = new UserInputPlayer();
            var measurements = new List<IMeasurement>
            {
                new GameEndedWithResultMeasurement(GameResult.WhiteWon),
                new GameEndedWithResultMeasurement(GameResult.Draw),
                new GameEndedWithResultMeasurement(GameResult.BlackWon),
                new AverageTimeToFinishGameMeasurement()
            };

            var experiment = new MtcsVsRandomExperiment
            {
                IndependentGameRunes = 1,
                Player1 = player1,
                Player2 = player2,
                Measurements = measurements
            };

            experiment.Perform();

            foreach (var measurement in measurements)
            {
                System.Console.WriteLine($"{measurement.Description} {measurement.Result}");
            }
            System.Console.ReadKey();
        }
    }
}
