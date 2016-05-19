using System.Collections.Generic;
using System.IO;
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
            System.Console.WriteLine(Directory.GetCurrentDirectory());

            var mtcsPlayer = new MTCSPlayer(new StaticBudgetAssignStrategy(20), new RandomChildSelectionStrategy(), new WinPercentageChildSelectionStrategy());
            var otherPlayer = new RandomMovePlayer();
            var measurements = new List<IMeasurement>
            {
                new GameEndedWithResultMeasurement(GameResult.WhiteWon),
                new GameEndedWithResultMeasurement(GameResult.BlackWon),
                new AverageTimeToFinishGameMeasurement()
            };

            var experiment = new MtcsVsOtherExperiment
            {
                IndependentGameRunes = 1,
                MtcsPlayer = mtcsPlayer,
                OtherPlayer = otherPlayer,
                MtcsPlayerPosition = 1,
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
