using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace theGreensun.Brain
{
    /// <summary>
    /// the <see cref="OutputNode"/> is the node which represents the output of a neuronal network
    /// </summary>
    [Serializable]
    public class OutputNode : NodeBase, INodeWithLinkedInput
    {
        /// <summary>
        ///  the <see cref="ILink"/> objects to the nodes which are the source for the input values
        /// </summary>
        private readonly List<ILink> _InputLinks = new List<ILink>();

        /// <summary>
        /// the value of the node 
        /// </summary>
        private Double _Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.OutputNode"/> class.
        /// </summary>
        /// <param name="name">the value for the name property.</param>
        public OutputNode(String name)
            : base(name)
        {
            _Value = 0.0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.NodeBase"/> class.
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> object which hold all the data</param>
        /// <param name="context">the <see cref="StreamingContext"/> object </param>
        public OutputNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _Value = info.GetDouble(nameof(Value));
            Int32 linkDataCount = info.GetInt32(nameof(LinkedInputNodeCount));

            for (Int32 i = 0; i < linkDataCount; ++i)
            {
                LinkData linkData = (LinkData)info.GetValue(nameof(LinkedInputNodes) + i, typeof(LinkData));

                Link link = new Link(linkData);
                link.TargetNode.AddInputNode(link);
                link.SourceNode.AddOutputNode(link);
            }
        }

        /// <summary>
        /// the input value 
        /// </summary>
        public virtual Double Value
        {
            get { return _Value; }
        }

        /// <summary>
        ///  the <see cref="ILink"/> objects to the nodes which are the source for the input values
        /// </summary>
        /// <value>The linked output nodes.</value>
        public IReadOnlyList<ILink> LinkedInputNodes
        {
            get { return _InputLinks.ToArray(); }
        }

        /// <summary>
        /// number ov nodes connected as input node
        /// </summary>
        /// <value>The linked input node count.</value>
        public Int32 LinkedInputNodeCount
        {
            get { return _InputLinks.Count; }
        }


        /// <summary>
        /// Sets the <see cref="Value"/> of the Node for the ticket
        /// </summary>
        /// <param name="value">the Value to set.</param>
        /// <param name="ticket">the ticket which is source for the value</param>
        public void SetValue(Guid ticket, Double value)
        {
            this.Ticket = ticket;
            this.SetPropertyFieldStruct(value, ref _Value, nameof(Value));
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">Info.</param>
        /// <param name="context">Context.</param>
        protected override void OnGetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Value), _Value);

            LinkData[] linkDatas = _InputLinks.Select(e => e.GetLinkData()).ToArray();

            info.AddValue(nameof(LinkedInputNodeCount), linkDatas.Length);
            for (Int32 i = 0; i < linkDatas.Length; ++i)
            {
                info.AddValue(nameof(LinkedInputNodes) + i, linkDatas[i]);
            }
        }

        /// <summary>
        /// Adds the node to the list of output.
        /// </summary>
        /// <param name="node">the target Node which gets linked to that node</param>
        public void AddInputNode(INodeWithLinkedOutput node)
        {
            if (node == null)
            {
                return;
            }

            ILink link = _InputLinks.FirstOrDefault(e => e.TargetNode.Id == node.Id);
            if (link == null)
            {
                link = new Link(node, this);
                _InputLinks.Add(link);
            }

            node.AddOutputNode(link);
        }

        /// <summary>
        /// Adds the node to the list of input.
        /// </summary>
        /// <param name="link">a object implementing <see cref="ILink"/>></param>
        public void AddInputNode(ILink link)
        {
            if (link.TargetNode.Id != this.Id)
            {
                throw new InvalidOperationException("Invalid link"); // todo move text to literals
            }

            ILink existingLink = _InputLinks.FirstOrDefault(e => e.SourceNode.Id == link.SourceNode.Id);

            if (existingLink != null)
            {
                if (existingLink.Id != link.Id)
                {
                    throw new InvalidOperationException("Duplicate Link"); // todo: move Text to literals
                }
                return;
            }

            _InputLinks.Add(link);
        }
    }
}
