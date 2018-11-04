using System;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace theGreensun.Brain
{
    /// <summary>
    /// base interface to all nodes 
    /// </summary>
    public interface INode : ISerializable, IDisposable
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid Id { get; }

        /// <summary>
        /// Gets the name of the node
        /// </summary>
        String Name { get; }
    }
}