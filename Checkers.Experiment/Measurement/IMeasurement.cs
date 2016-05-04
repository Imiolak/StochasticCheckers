using Checkers.Engine;

namespace Checkers.Experiment.Measurement
{
    public interface IMeasurement
    {
        string Description { get; }
        double Result { get; }

        void AddEntry(IGame game);
    }
}
