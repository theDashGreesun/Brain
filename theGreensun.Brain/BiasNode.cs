using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace theGreensun.Brain
{
    /// <summary>
    /// the node which is used to set the iputs of an neuronal network
    /// </summary>
    [Serializable]
    public class BiasNode : NodeBase, INodeWithLinkedOutput
    {
        /// <summary>
        ///  the <see cref="ILink"/> objects to the nodes which are the source for the input values
        /// </summary>
        private readonly List<ILink> _OutputLinks = new List<ILink>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.InputNode"/> class.
        /// </summary>
        /// <param name="name">the value for the name property.</param>
        public BiasNode(String name)
            : base(name)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.NodeBase"/> class.
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> object which hold all the data</param>
        /// <param name="context">the <see cref="StreamingContext"/> object </param>
        public BiasNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            
        }

        /// <summary>
        /// the input value 
        /// </summary>
        public virtual Double Value
        {
            get { return 1.0; }
        }

        /// <summary>
        ///  the <see cref="ILink"/> objects to the nodes which are the source for the input values
        /// </summary>
        /// <value>The linked output nodes.</value>
        public IReadOnlyList<ILink> LinkedOutputNodes
        {
            get { return _OutputLinks.ToArray(); }
        }

        /// <summary>
        /// Adds the node to the list of output.
        /// </summary>
        /// <param name="node">the target Node which gets linked to that node</param>
        public void AddOutputNode(INodeWithLinkedInput node)
        {
            if (node == null)
            {
                return;
            }

            ILink link = _OutputLinks.FirstOrDefault(e => e.TargetNode.Id == node.Id);
            if (link == null)
            {
                link = new Link(this, node);
                _OutputLinks.Add(link);
            }

            node.AddInputNode(link);
        }

        /// <summary>
        /// Adds the node to the list of input.
        /// </summary>
        /// <param name="link">a object implementing <see cref="ILink"/>></param>
        public void AddOutputNode(ILink link)
        {
            if (link.SourceNode.Id != this.Id)
            {
                throw new InvalidOperationException("Invalid link"); // todo move text to literals
            }

            ILink existingLink = _OutputLinks.FirstOrDefault(e => e.TargetNode.Id == link.TargetNode.Id);

            if (existingLink != null)
            {
                if (existingLink.Id != link.Id)
                {
                    throw new InvalidOperationException("Duplicate Link"); // todo: move Text to literals
                }
                return;
            }

            _OutputLinks.Add(link);
        }
    }
}
