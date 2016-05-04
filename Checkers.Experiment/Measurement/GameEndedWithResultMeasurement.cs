using Checkers.Engine;
using Checkers.Engine.Display;

namespace Checkers.Experiment.Measurement
{
    public class GameEndedWithResultMeasurement : IMeasurement
    {
        private readonly GameResult _result;
        private int _winCount;

        public GameEndedWithResultMeasurement(GameResult result)
        {
            _result = result;
        }

        public string Description => $"Games ended with result {_result}";

        public double Result => _winCount;

        public void AddEntry(IGame game)
        {
            if (game.Result == _result)
                _winCount++;
        }
    }
}
