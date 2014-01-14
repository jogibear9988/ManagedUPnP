//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System;
using System.Text;
using System.Xml;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates a ordered description dictionary with built in ToString formatting.
    /// </summary>
    /// <typeparam name="TKey">The type for the key.</typeparam>
    /// <typeparam name="TValue">The type for the value.</typeparam>
    public abstract class FormattedOrderedDescriptionDictionary<TKey, TValue> : 
        OrderedDescriptionDictionary<TKey, TValue> where TValue : Description
    {
        #region Initialisation

        /// <summary>
        /// Creates a new formatted description dictionary.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        public FormattedOrderedDescriptionDictionary(Description parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Creates a new formatted description dictionary from a reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader.</param>
        public FormattedOrderedDescriptionDictionary(Description parent, XmlTextReader reader)
            : base(parent, reader)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the description to a string.
        /// </summary>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The string representation for the description.</returns>
        public override string ToString(int indent)
        {
            if (this.Count > 0)
            {
                string lsIndent = Indent(indent);
                StringBuilder lsbBuilder = new StringBuilder();

                lsbBuilder.Append(lsIndent);
                lsbBuilder.AppendLine("{");

                foreach (Description ldDesc in this.Values)
                    lsbBuilder.AppendLine(ldDesc.ToString(indent + 1));

                lsbBuilder.Append(lsIndent);
                lsbBuilder.Append("}");

                lsbBuilder.Append(base.ToString(indent).LineBefore());

                return lsbBuilder.ToString();
            }
            else
                return String.Empty;
        }

        #endregion
    }
}

#endif