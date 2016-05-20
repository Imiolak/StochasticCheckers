using System.Collections.Generic;
using System.IO;
using Checkers.Experiment.Measurement;

namespace Checkers.Experiment.Output
{
    public class FileMeasurementsOutput : IMeasurementsOutput
    {
        private readonly string _filePath;

        public FileMeasurementsOutput(string filePath)
        {
            _filePath = filePath;
        }
        
        public void Write(IEnumerable<IMeasurement> measurements)
        {
            var directory = Path.GetDirectoryName(_filePath);
            Directory.CreateDirectory(directory);
            var stream = File.Create(_filePath);
            var writer = new StreamWriter(stream);

            foreach (var measurement in measurements)
            {
                writer.WriteLine($"{measurement.Description} {measurement.Result}");
            }

            writer.Flush();
            writer.Close();
        }
    }
}
