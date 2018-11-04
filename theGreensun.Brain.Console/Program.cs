using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace theGreensun.Brain.BrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            INode[] nodes = new INode[4]; 

            BiasNode biasNode = new BiasNode("Bias");
            InputNode inputNode = new InputNode("Input");
            MiddleNode middleNode = new MiddleNode("Middle");
            OutputNode outputNode = new OutputNode("Output");

            nodes[0] = biasNode;
            nodes[1] = inputNode;
            nodes[2] = outputNode;
            nodes[3] = middleNode;

            middleNode.AddInputNode(biasNode);
            middleNode.AddInputNode(inputNode);
            middleNode.AddOutputNode(outputNode);


            MemoryStream stream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, nodes);

            foreach (INode node in nodes)
            {
                node.Dispose();
            }

            stream.Position = 0;

            INode[] streamedNodes = (INode[])binaryFormatter.Deserialize(stream);

            foreach(INode node in streamedNodes)
            {
                Console.WriteLine(node.Name);
            }
        }
    }
}
