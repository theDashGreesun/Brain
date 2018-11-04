using System;
using System.Collections.Generic;

namespace theGreensun.Brain
{
    /// <summary>
    /// the interface for nodes which have inputs that are linked to <see cref="INodeWithLinkedOutput"/>
    /// </summary>
    public interface INodeWithLinkedInput : INode
    {
        /// <summary>
        /// the <see cref="ILink"/> objects to the nodes which are the source for the input valuesÍ
        /// </summary>
        /// <value>The linked input nodes.</value>
        IReadOnlyList<ILink> LinkedInputNodes { get; }

        /// <summary>
        /// Adds the node to the list of input.
        /// </summary>
        /// <param name="node">a Node implementing <see cref="INodeWithLinkedOutput"/></param>
        void AddInputNode(INodeWithLinkedOutput node);

        /// <summary>
        /// Adds the node to the list of input.
        /// </summary>
        /// <param name="link">a object implementing <see cref="ILink"/>></param>
        void AddInputNode(ILink link);
    }
}
