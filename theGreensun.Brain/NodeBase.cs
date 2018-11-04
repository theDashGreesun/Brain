using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace theGreensun.Brain
{
    /// <summary>
    /// the base class for all nodes
    /// </summary>
    [Serializable]
    public abstract class NodeBase : BrainObject, INode, IDisposable
    {
        /// <summary>
        /// the collection with all nodes
        /// </summary>
        private static readonly Dictionary<Guid, INode> _NodeDictionary = new Dictionary<Guid, INode>();

        /// <summary>
        /// The field for the name
        /// </summary>
        private readonly String _Name;

        /// <summary>
        /// The disposed flag
        /// </summary>
        private Boolean _Disposed = false;

        /// <summary>
        /// the current ticket id
        /// </summary>
        private Guid _Ticket;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.NodeBase"/> class.
        /// </summary>
        protected NodeBase(String name)
            : base(Guid.NewGuid())
        {
            _Name = name;

            _NodeDictionary.Add(this.Id, this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.NodeBase"/> class.
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> object which hold all the data</param>
        /// <param name="context">the <see cref="StreamingContext"/> object </param>
        protected NodeBase(SerializationInfo info, StreamingContext context)
            : base ((Guid)info.GetValue(nameof(Id), typeof(Guid)))
        {
            _Name = info.GetString(nameof(Name));
        }

        /// <summary>
        /// the finalizer
        /// </summary>
        ~NodeBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// returns the name of the node
        /// </summary>
        public String Name
        {
            get { return _Name; }
        }

        /// <summary>
        /// Gets or sets the ticket value.
        /// </summary>
        /// <value>The ticket.</value>
        public Guid Ticket
        {
            get { return _Ticket; }
            protected set { this.SetPropertyFieldStruct(value, ref _Ticket, nameof(Ticket)); }
        }

        /// <summary>
        /// Gets the object data.
        /// </summary>
        /// <param name="info">Info.</param>
        /// <param name="context">Context.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), this.Id);
            info.AddValue(nameof(Name), _Name);

            this.OnGetObjectData(info, context);
        }

        /// <summary>
        /// get object data of derived classes
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> object which hold all the data</param>
        /// <param name="context">the <see cref="StreamingContext"/> object </param>
        protected virtual void OnGetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        /// <summary>
        /// performs the disposing of the object
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            Boolean disposed = false;
            lock (this.LockObject)
            {
                disposed = _Disposed;
                _Disposed = true;
            }

            if (!disposed)
            {
                if (_NodeDictionary.ContainsKey(this.Id))
                {
                    _NodeDictionary.Remove(this.Id);
                }

            }
        }

        /// <summary>
        /// Releases all resource used by the <see cref="T:theGreensun.Brain.NodeBase"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose()"/> when you are finished using the <see cref="T:theGreensun.Brain.NodeBase"/>. The
        /// <see cref="Dispose()"/> method leaves the <see cref="T:theGreensun.Brain.NodeBase"/> in an unusable state.
        /// After calling <see cref="Dispose()"/>, you must release all references to the
        /// <see cref="T:theGreensun.Brain.NodeBase"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:theGreensun.Brain.NodeBase"/> was occupying.</remarks>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// returns the node which implements <see cref="INodeWithLinkedInput"/> and teh provided <paramref name="id"/>
        /// </summary>
        /// <param name="id">Identifier of the node which is requested</param>
        /// <param name="outNode">Out node.</param>
        public static void GetNode(Guid id, out INodeWithLinkedInput outNode)
        {
            INode node;
            if (!_NodeDictionary.TryGetValue(id, out node))
            {
                throw new IndexOutOfRangeException(); // todo
            }

            if (!(node is INodeWithLinkedInput))
            {
                throw new InvalidCastException(); // todo
            }

            outNode = node as INodeWithLinkedInput;
        }

        /// <summary>
        /// returns the node which implements <see cref="INodeWithLinkedOutput"/> and teh provided <paramref name="id"/>
        /// </summary>
        /// <param name="id">Identifier of the node which is requested</param>
        /// <param name="outNode">Out node.</param>
        public static void GetNode(Guid id, out INodeWithLinkedOutput outNode)
        {
            INode node;
            if (!_NodeDictionary.TryGetValue(id, out node))
            {
                throw new IndexOutOfRangeException(); // todo
            }

            if (!(node is INodeWithLinkedOutput))
            {
                throw new InvalidCastException(); // todo
            }

            outNode = node as INodeWithLinkedOutput;
        }
    }
}
