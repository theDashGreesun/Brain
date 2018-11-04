using System;
using System.Runtime.Serialization;

namespace theGreensun.Brain
{
    /// <summary>
    /// the interface to a link 
    /// </summary>
    public interface ILink
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid Id { get; }

        /// <summary>
        /// the <see cref="INodeWithLinkedOutput"/> which is the source of the value to transfer
        /// </summary>
        /// <value>The source node.</value>
        INodeWithLinkedOutput SourceNode { get; }

        /// <summary>
        /// the <see cref="INodeWithLinkedInput"/> which is the traget of the value
        /// </summary>
        /// <value>The target node.</value>
        INodeWithLinkedInput TargetNode { get; }

        /// <summary>
        /// the weight of the link
        /// </summary>
        Double Weight { get; set; }

        /// <summary>
        /// Gets the data of the link in an struct containig id's only
        /// </summary>
        /// <returns>an <see cref="LinkData"/> instance</returns>
        LinkData GetLinkData();
    }
}
