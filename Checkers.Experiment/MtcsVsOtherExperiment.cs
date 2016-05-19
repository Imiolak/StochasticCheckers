using System;
using System.Collections.Generic;
using System.IO;
using Checkers.Algorithms;
using Checkers.Algorithms.MTCS.Output;
using Checkers.Engine;
using Checkers.Experiment.Measurement;

namespace Checkers.Experiment
{
    public class MtcsVsOtherExperiment : IExperiment
    {
        public int IndependentGameRunes { get; set; }
        public MTCSPlayer MtcsPlayer { get; set; }
        public IPlayer OtherPlayer { get; set; }
        public int MtcsPlayerPosition { get; set; }
        public IEnumerable<IMeasurement> Measurements { get; set; }

        public void Perform()
        {
            for (var i = 0; i < IndependentGameRunes; i++)
            {
                var game = MtcsPlayerPosition == 1
                    ? new CheckersGame(MtcsPlayer, OtherPlayer)
                    : new CheckersGame(OtherPlayer, MtcsPlayer); 
                
                game.Start();

                foreach (var mesurement in Measurements)
                {
                    mesurement.AddEntry(game);
                }

                var pathToResults = "..\\..\\..\\Results\\";
                var filename = DateTime.Now.ToString("yyyyMMdd HHmmss") + "\\history";
                var output = new FileTreeOutput(pathToResults + filename, new BottomUpParser());

                output.Write(MtcsPlayer.GetTree());
            }
        }
    }
}
