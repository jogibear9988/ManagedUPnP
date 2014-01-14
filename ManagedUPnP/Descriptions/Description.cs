//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

#if !Exclude_Descriptions || !Exclude_CodeGen

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace ManagedUPnP.Descriptions
{
    /// <summary>
    /// Encapsulates an XML element description.
    /// </summary>
    public abstract class Description
    {
        #region Protected Static Locals

        /// <summary>
        /// Stores the used properties for a type.
        /// </summary>
        protected static Dictionary<Type, HashSet<string>> mdUsedPropCache = new Dictionary<Type, HashSet<string>>();

        #endregion

        #region Protected Locals

        /// <summary>
        /// Stores the properties for this object.
        /// </summary>
        protected Dictionary<string, string> mdProperties = new Dictionary<string, string>();

        /// <summary>
        /// The parent description object, or null if root description.
        /// </summary>
        protected Description mdParent;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new description.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        public Description(Description parent)
        {
            mdParent = parent;
            Initialise();
        }

        /// <summary>
        /// Creates a new description from an XML reader.
        /// </summary>
        /// <param name="parent">The parent description object, or null if root description.</param>
        /// <param name="reader">The XML reader to get the information from.</param>
        public Description(Description parent, XmlTextReader reader)
            : this(parent)
        {
            ProcessReader(reader);
        }

        /// <summary>
        /// Initialises any fields for the description.
        /// </summary>
        protected virtual void Initialise()
        {
        }

        #endregion

        #region Protected Abstract Methods

        /// <summary>
        /// Gets whether the current node for a reader marks the start of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        protected abstract bool IsStartNode(XmlTextReader reader);

        /// <summary>
        /// Gets whether the current node for a reader marks the start of this description.
        /// </summary>
        /// <param name="reader">The XML text reader.</param>
        /// <returns>True or false.</returns>
        protected abstract bool IsEndNode(XmlTextReader reader);

        #endregion

        #region Protected Methods

        /// <summary>
        /// Processes the description from an XML reader.
        /// </summary>
        /// <param name="reader">The XML reader.</param>
        protected virtual void ProcessReader(XmlTextReader reader)
        {
            if (IsStartNode(reader))
            {
                string lsLastName = string.Empty;

                while (reader.Read())
                {
                    if (!UseNode(reader, lsLastName))
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                lsLastName = reader.Name;
                                break;

                            case XmlNodeType.Text:
                                mdProperties[lsLastName] = reader.Value;
                                break;

                            case XmlNodeType.EndElement:
                                if (reader.Name == lsLastName) lsLastName = string.Empty;
                                break;
                        }
                    }

                    if (IsEndNode(reader)) break;
                }
            }
            else
                throw new InvalidOperationException(
                    string.Format(
                        "The node {0} is not a valid node for {1}",
                        reader.Name, this.GetType().Name));
        }

        /// <summary>
        /// Gets the string for an indent.
        /// </summary>
        /// <param name="indent">The indentation for the string.</param>
        /// <returns>The indent string.</returns>
        protected string Indent(int indent)
        {
            return new String(' ', indent * 4);
        }

        /// <summary>
        /// Uses an XML node for this description.
        /// </summary>
        /// <param name="reader">The XML reader to use the node from.</param>
        /// <param name="lastNodeName">The last node name.</param>
        /// <returns>True if the node was processed false otherwise.</returns>
        protected virtual bool UseNode(XmlTextReader reader, string lastNodeName)
        {
            return false;
        }

        /// <summary>
        /// Gets a parent from a certain generation.
        /// </summary>
        /// <param name="generation">The generation to get the parent for (0 == direct parent).</param>
        /// <returns>The description of the parent object or null if not available.</returns>
        protected Description GetParentFrom(int generation)
        {
            Description ldParent = Parent;

            while (generation > 0 && ldParent != null)
            {
                ldParent = ldParent.Parent;
                generation--;
            }

            return ldParent;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a property value as an integer.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <returns>An integer.</returns>
        public int GetPropertyInt(string name, int defaultValue = 0)
        {
            string lsValue;
            if (mdProperties.TryGetValue(name, out lsValue))
            {
                int liValue;
                if (int.TryParse(lsValue, out liValue))
                    return liValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets a property value as a double.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="defaultValue">The default value if conversion fails.</param>
        /// <returns>A double.</returns>
        public double GetPropertyDouble(string name, double defaultValue = 0)
        {
            string lsValue;
            if (mdProperties.TryGetValue(name, out lsValue))
            {
                double ldValue;
                if (double.TryParse(lsValue, out ldValue))
                    return ldValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets a property value as a string.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="defaultValue">The default value if it fails.</param>
        /// <returns>A string.</returns>
        public string GetPropertyString(string name, string defaultValue = null)
        {
            if (defaultValue == null) defaultValue = String.Empty;

            string lsValue;
            if (mdProperties.TryGetValue(name, out lsValue))
                return lsValue;
            else
                return defaultValue;
        }

        /// <summary>
        /// Converts the description to a string.
        /// </summary>
        /// <returns>The string representation for the description.</returns>
        public override string ToString()
        {
            return ToString(0);
        }

        /// <summary>
        /// Converts the description to a string.
        /// </summary>
        /// <param name="indent">The indent for the string.</param>
        /// <returns>The string representation for the description.</returns>
        public virtual string ToString(int indent)
        {
            IEnumerable<KeyValuePair<string, string>> lhsUnused = GetUnusedProperties();

            if (lhsUnused.Count() > 0)
            {
                StringBuilder lsbBuilder = new StringBuilder();
                string lsIndent = Indent(indent);

                lsbBuilder.Append(lsIndent);
                lsbBuilder.Append("( ");

                string lsIndentPlus1 = Indent(indent + 1);
                bool lbFirst = true;
                foreach (KeyValuePair<string, string> lkvProp in lhsUnused)
                {
                    if (!lbFirst)
                        lsbBuilder.Append(", ");
                    else
                        lbFirst = false;

                    lsbBuilder.AppendLine();
                    lsbBuilder.Append(String.Format("{0}{1} = '{2}'", lsIndentPlus1, lkvProp.Key, lkvProp.Value));
                }

                lsbBuilder.AppendLine();

                lsbBuilder.Append(lsIndent);
                lsbBuilder.Append(")");

                return lsbBuilder.ToString();
            }
            else
                return String.Empty;
        }

        /// <summary>
        /// Gets the unused properties by the class and their values.
        /// </summary>
        /// <returns>The enumerable list of properties and their values.</returns>
        public IEnumerable<KeyValuePair<string, string>> GetUnusedProperties()
        {
            HashSet<string> lhsUsed = GetUsedPropertyNames();

            return mdProperties.Where(lkvValue => { return !lhsUsed.Contains(lkvValue.Key); });
        }

        /// <summary>
        /// Gets a hashset of the used properties by this description.
        /// </summary>
        /// <returns>The hashset of used properties.</returns>
        public HashSet<string> GetUsedPropertyNames()
        {
            HashSet<string> lhsReturn;
            if (!mdUsedPropCache.TryGetValue(this.GetType(), out lhsReturn))
            {
                lhsReturn = new HashSet<string>();

                object[] loAtts = this.GetType().GetCustomAttributes(typeof(UsesPropertyAttribute), true);
                foreach (UsesPropertyAttribute laAttr in loAtts) lhsReturn.Add(laAttr.Name);

                MemberInfo[] lmMembers = this.GetType().GetMembers(
                    BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                );

                foreach (MemberInfo lmMember in lmMembers)
                {
                    loAtts = lmMember.GetCustomAttributes(typeof(UsesPropertyAttribute), true);
                    foreach (UsesPropertyAttribute laAttr in loAtts) lhsReturn.Add(laAttr.Name);
                }

                mdUsedPropCache[this.GetType()] = lhsReturn;
            }

            return lhsReturn;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the parent description object or null if this is a root description.
        /// </summary>
        public Description Parent
        {
            get
            {
                return mdParent;
            }
        }

        #endregion
    }
}

#endif