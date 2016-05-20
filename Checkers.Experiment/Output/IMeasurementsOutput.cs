using System.Collections;
using System.Collections.Generic;
using Checkers.Experiment.Measurement;

namespace Checkers.Experiment.Output
{
    public interface IMeasurementsOutput
    {
        void Write(IEnumerable<IMeasurement> measurements);
    }
}
