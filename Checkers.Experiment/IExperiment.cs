using System.Collections.Generic;
using Checkers.Experiment.Measurement;

namespace Checkers.Experiment
{
    public interface IExperiment
    {
        IEnumerable<IMeasurement> Measurements { get; set; }

        void Perform();
    }
}
