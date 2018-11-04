using System;
using System.Collections.Generic;

namespace theGreensun.Brain
{
    /// <summary>
    /// the interface for nodes which have outputs that are linked to <see cref="INodeWithLinkedInput"/>
    /// </summary>
    public interface INodeWithLinkedOutput :INode
    {
        /// <summary>
        /// the <see cref="ILink"/> objects to the nodes which are the source for the input values
        /// </summary>
        /// <value>The linked input nodes.</value>
        IReadOnlyList<ILink> LinkedOutputNodes { get; }

        /// <summary>
        /// Adds the node to the list of output.
        /// </summary>
        /// <param name="node">Node.</param>
        void AddOutputNode(INodeWithLinkedInput node);

        /// <summary>
        /// Adds the node to the list of input.
        /// </summary>
        /// <param name="link">a object implementing <see cref="ILink"/>></param>
        void AddOutputNode(ILink link);
    }
}
