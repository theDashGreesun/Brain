using System;
using System.ComponentModel;

namespace theGreensun.Brain
{
    /// <summary>
    /// base class for all brain objects
    /// </summary>
    public class BrainObject : INotifyPropertyChanged
    {
        /// <summary>
        /// the event which gets fired when a property has been changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// the object for thead save operations
        /// </summary>
        private readonly Object _LockObject = new Object();

        /// <summary>
        /// the field for the identifier
        /// </summary>
        private readonly Guid _Id;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:theGreensun.Brain.BrainObject"/> class.
        /// </summary>
        /// <param name="id">the initial value for the <see cref="Id"/> property</param>
        protected BrainObject(Guid id)
        {
            _Id = id;
        }

        /// <summary>
        /// returns the object whcih can be used for thread save operations
        /// </summary>
        /// <value>The lock object.</value>
        protected Object LockObject
        {
            get { return _LockObject; }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public Guid Id
        {
            get { return _Id; }
        }

        /// <summary>
        /// Sets the field of a property with change checking. The event will be fired if changed
        /// </summary>
        /// <returns><c>true</c>, if property field was set, <c>false</c> otherwise.</returns>
        /// <param name="value">the new Value.</param>
        /// <param name="field">the field which is source of the check and target for the new value</param>
        /// <param name="propertyName">the name of the related Property.</param>
        /// <typeparam name="T">The type of the value and the field</typeparam>
        protected Boolean SetPropertyFieldObject<T>(T value, ref T field, String propertyName)
            where T : class
        {
            if (value == null)
            {
                if (field == null)
                {
                    return false;
                }
            }
            else if (value.Equals(field))
            {
                return false;
            }

            field = value;

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        /// <summary>
        /// Sets the field of a property with change checking. The event will be fired if changed
        /// </summary>
        /// <returns><c>true</c>, if property field was set, <c>false</c> otherwise.</returns>
        /// <param name="value">the new Value.</param>
        /// <param name="field">the field which is source of the check and target for the new value</param>
        /// <param name="propertyName">the name of the related Property.</param>
        /// <typeparam name="T">The type of the value and the field</typeparam>
        protected Boolean SetPropertyFieldStruct<T>(T value, ref T field, String propertyName)
            where T : struct
        {
            if (value.Equals(field))
            {
                return false;
            }

            field = value;

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }
    }
}
