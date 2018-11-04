using System;
using System.Runtime.Serialization;

namespace theGreensun.Brain
{
    /// <summary>
    /// a struct which contains the configuration data data of a link. this is used
    /// for serialization of the link 
    /// </summary>
    [Serializable]
    public class LinkData : ISerializable
    {
        /// <summary>
        /// the constructor with initialization of the properties
        /// </summary>
        /// <param name="id">the initial value for the <see cref="Id"/>.</param>
        /// <param name="sourceNodeId">the initial value for the <see cref="SourceNodeId"/>.</param>
        /// <param name="targetNodeId">the initial value for the <see cref="TargetNodeId"/>.</param>
        /// <param name="weight">the initial value for the <see cref="Weight"/>.</param>
        public LinkData(Guid id, Guid sourceNodeId, Guid targetNodeId, Double weight)
        {
            this.Id = id;
            this.SourceNodeId = sourceNodeId;
            this.TargetNodeId = targetNodeId;
            this.Weight = weight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.LinkData"/> struct.
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> object which hold all the data</param>
        /// <param name="context">the <see cref="StreamingContext"/> object </param>
        public LinkData(SerializationInfo info, StreamingContext context)
        {
            this.Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
            this.SourceNodeId = (Guid)info.GetValue(nameof(SourceNodeId), typeof(Guid));
            this.TargetNodeId = (Guid)info.GetValue(nameof(TargetNodeId), typeof(Guid));
            this.Weight = (Double)info.GetValue(nameof(Weight), typeof(Double));
        }

        /// <summary>
        /// Gets the object data for serialization. 
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> object which hold all the data</param>
        /// <param name="context">the <see cref="StreamingContext"/> object </param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), this.Id);
            info.AddValue(nameof(SourceNodeId), this.SourceNodeId);
            info.AddValue(nameof(TargetNodeId), this.TargetNodeId);
            info.AddValue(nameof(Weight), this.Weight);
        }

        /// <summary>
        /// the id of the link
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// the id of the Source Node
        /// </summary>
        /// <value>The source node identifier.</value>
        public Guid SourceNodeId { get; private set; }

        /// <summary>
        /// the id if the Target node
        /// </summary>
        /// <value>The target node identifier.</value>
        public Guid TargetNodeId { get; private set; }

        /// <summary>
        /// the weight of the link
        /// </summary>
        /// <value>The weight.</value>
        public Double Weight { get; private set; }
    }
}
