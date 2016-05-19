using System.IO;

namespace Checkers.Algorithms.MTCS.Output
{
    public class FileTreeOutput : TreeOutputBase
    {
        private readonly string _filePath;

        public FileTreeOutput(string filePath, ITreeParser parser) : base(parser)
        {
            _filePath = filePath;
        }
        
        public override void Write(MTCSTree tree)
        {
            var directory = Path.GetDirectoryName(_filePath);
            Directory.CreateDirectory(directory);
            var stream = File.Create(_filePath);
            var writer = new StreamWriter(stream);
            var nodes = TreeParser.Parse(tree);

            foreach (var node in nodes)
            {
                if (node.Action != null)
                    writer.WriteLine(node.Action.ToString());
            }

            writer.Flush();
            writer.Close();
        }
    }
}
