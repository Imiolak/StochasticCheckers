using System;
using Checkers.Engine;

namespace Checkers.Experiment.Measurement
{
    public class AverageTimeToFinishGameMeasurement : IMeasurement
    {
        private int _entries;
        private TimeSpan _totalTime = TimeSpan.Zero;

        public string Description => "Average time to finish single game (s)";

        public double Result => _totalTime.TotalSeconds/_entries;

        public void AddEntry(IGame game)
        {
            _entries++;
            _totalTime += game.TimeElapsed;
        }
    }
}
