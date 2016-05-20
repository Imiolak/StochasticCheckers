using System;
using System.Collections.Generic;
using Checkers.Algorithms;
using Checkers.Engine;
using Checkers.Experiment.Measurement;
using Checkers.Experiment.Output;
using BottomUpParser = Checkers.Experiment.Output.BottomUpParser;
using FileTreeOutput = Checkers.Experiment.Output.FileTreeOutput;

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
            var pathToResultsFolder = "..\\..\\..\\Results\\" + DateTime.Now.ToString("yyyyMMdd HHmmss");

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
                
                var historyFilePath = pathToResultsFolder + $"\\history{i}.txt";
                var treeOutput = new FileTreeOutput(historyFilePath, new BottomUpParser());
                treeOutput.Write(MtcsPlayer.GetTree());
            }

            var measurementsFilePath = pathToResultsFolder + "\\measurements.txt";
            var measurementsOutput = new FileMeasurementsOutput(measurementsFilePath);
            measurementsOutput.Write(Measurements);
        }
    }
}
