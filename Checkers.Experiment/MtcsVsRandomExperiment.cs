using System.Collections.Generic;
using Checkers.Engine;
using Checkers.Experiment.Measurement;

namespace Checkers.Experiment
{
    public class MtcsVsRandomExperiment : IExperiment
    {
        public int IndependentGameRunes { get; set; }
        public IPlayer Player1 { get; set; }
        public IPlayer Player2 { get; set; }
        public IEnumerable<IMeasurement> Measurements { get; set; }

        public void Perform()
        {
            for (var i = 0; i < IndependentGameRunes; i++)
            {
                var game = new CheckersGame(Player1, Player2);
                game.Start();

                foreach (var mesurement in Measurements)
                {
                    mesurement.AddEntry(game);
                }
            }
        }
    }
}
