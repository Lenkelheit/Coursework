using System.Collections.Generic;
using System.Reflection;

namespace DataAccess.Entities
{
    /// <summary>
    /// Represents abstract class for entities
    /// </summary>
    public abstract class EntityBase : Interfaces.IEntity
    {
        // PROPERTIES
        /// <summary>
        /// Unique identifier
        /// </summary>
        public abstract System.Guid Id { get; set; }

        // METHODS
#warning bug #186
        /*
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object, otherwise — false.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when <paramref name="obj"/> is null
        /// </exception>
        public override bool Equals(object obj)
        {
            // passed object is null, exception
            if (obj == null) throw new System.ArgumentNullException(nameof(obj));

            // same reference, return true
            if (object.ReferenceEquals(this, obj)) return true;

            // gets passed object's type
            System.Type objectType = obj.GetType();

            // different object's types, return false
            if (objectType != this.GetType()) return false;

            // check each property on equality                       
            foreach (PropertyInfo property in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // gets properties' values
                object currentProperty = property.GetValue(this);
                object objectProperty = property.GetValue(obj);

                // skip if both null
                if (currentProperty == null && objectProperty == null) continue;

                // comparing properties

                // if one is null while another is not return false
                if (currentProperty == null || objectProperty == null) return false;

                // check if is IEnumerable                
                if (currentProperty is IEnumerable<object>)
                {
                    // check collection's values on equality
                    if (!System.Linq.Enumerable.SequenceEqual((IEnumerable<object>)currentProperty, (IEnumerable<object>)objectProperty))
                    {
                        // collections are not equal
                        return false;
                    }
                 
                }
                else if (!currentProperty.Equals(objectProperty))
                {
                    // values are not equal
                    return false;
                }

            }

            // all properties' values are equal, return true
            return true;
        }
        */
        /// <summary>
        /// Gets hash code
        /// </summary>
        /// <returns>
        /// Hash code
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region To String
        /// <summary>
        /// Gets entity as a string formated in specific way
        /// </summary>
        /// <param name="entityStringFormat">
        /// Entity format option
        /// </param>
        /// <returns>
        /// Entity represented as string
        /// </returns>
        public string ToString(Enums.EntityStringFormat entityStringFormat)
        {
            return ToString(entityStringFormat.ToString(), null);
        }
        /// <summary>
        /// Formats the value of the entity using the specified format.
        /// </summary>
        /// <param name="format"> 
        /// The format to use
        /// <para> -or- </para>
        /// A null reference to use the default format defined for the type of the System.IFormattable implementation.
        /// </param>
        /// <param name="formatProvider">
        /// The provider to use to format the value.
        /// <para>-or-</para>
        /// A null reference to obtain the numeric format information from the current locale setting of the operating system.
        /// </param>
        /// <returns>
        /// The value of the entity in the specified format
        /// </returns>
        public string ToString(string format, System.IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format)) return ToString();

            if (format == Enums.EntityStringFormat.Name.ToString()) return GetName();
            else if (format == Enums.EntityStringFormat.BriefInfo.ToString()) return GetBriefInfo();
            else if (format == Enums.EntityStringFormat.ToString.ToString()) return ToString();

            return ToString();
        }
        #region format method
        /// <summary>
        /// Gets entity name
        /// </summary>
        /// <returns>Entity's name</returns>
        protected abstract string GetName();
        /// <summary>
        /// Gets brief information about entity
        /// </summary>
        /// <returns>Brief information about entity</returns>
        protected abstract string GetBriefInfo();
        #endregion
        #endregion
    }
}
