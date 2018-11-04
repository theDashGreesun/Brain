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
    public class InputNode : BiasNode
    {
        /// <summary>
        ///  the <see cref="ILink"/> objects to the nodes which are the source for the input values
        /// </summary>
        private readonly List<ILink> _OutputLinks = new List<ILink>();

        /// <summary>
        /// the value of the node 
        /// </summary>
        private double _Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.InputNode"/> class.
        /// </summary>
        /// <param name="name">the value for the name property.</param>
        public InputNode(String name)
            : base(name)
        {
            _Value = 0.0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.NodeBase"/> class.
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> object</param>
        /// <param name="context">the <see cref="StreamingContext"/> object which hold all the data </param>
        public InputNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _Value = info.GetDouble(nameof(Value));
        }

        /// <summary>
        /// the input value 
        /// </summary>
        public override Double Value
        {
            get { return _Value; }
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
        }
    }
}
