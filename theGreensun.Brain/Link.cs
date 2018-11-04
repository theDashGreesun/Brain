using System;
using System.Runtime.Serialization;

namespace theGreensun.Brain
{
    /// <summary>
    /// the object which link two nodes
    /// </summary>
    public class Link : BrainObject, ILink
    {
        /// <summary>
        /// the <see cref="INodeWithLinkedOutput"/> which is the source of the value to transfer
        /// </summary>
        /// <value>The source node.</value>
        private readonly INodeWithLinkedOutput _SourceNode;

        /// <summary>
        /// the <see cref="INodeWithLinkedInput"/> which is the traget of the value
        /// </summary>
        /// <value>The target node.</value>
        private readonly INodeWithLinkedInput _TargetNode;

        /// <summary>
        /// the value of the node 
        /// </summary>
        private Double _Weight;

        /// <summary>
        /// constructor with initialization of the target and the source
        /// </summary>
        /// <param name="sourceNode">the Source node.</param>
        /// <param name="targetNode">the Target node.</param>
        public Link(INodeWithLinkedOutput sourceNode, INodeWithLinkedInput targetNode)
            : base(Guid.NewGuid())
        {
            _SourceNode = sourceNode;
            _TargetNode = targetNode;
        }

        /// <summary>
        /// constructor with initialization of the target and the source
        /// </summary>
        internal Link(LinkData linkData)
            : base(linkData.Id)
        {
            NodeBase.GetNode(linkData.SourceNodeId, out _SourceNode);
            NodeBase.GetNode(linkData.TargetNodeId, out _TargetNode);
            _Weight = linkData.Weight;
        }

        /// <summary>
        /// the <see cref="INodeWithLinkedOutput"/> which is the source of the value to transfer
        /// </summary>
        /// <value>The source node.</value>
        public INodeWithLinkedOutput SourceNode 
        { 
            get { return _SourceNode; }
        }

        /// <summary>
        /// the <see cref="INodeWithLinkedInput"/> which is the traget of the value
        /// </summary>
        /// <value>The target node.</value>
        public INodeWithLinkedInput TargetNode 
        {
            get { return _TargetNode; }
        }

        /// <summary>
        /// the input value 
        /// </summary>
        public Double Weight
        {
            get { return _Weight; }
            set { this.SetPropertyFieldStruct(value, ref _Weight, nameof(Weight)); }
        }

        /// <summary>
        /// Gets the data of the link in an struct containig id's only
        /// </summary>
        /// <returns>an <see cref="LinkData"/> instance</returns>
        public LinkData GetLinkData()
        {
            return new LinkData(this.Id, _SourceNode.Id, _TargetNode.Id, _Weight);
        }
    }
}
