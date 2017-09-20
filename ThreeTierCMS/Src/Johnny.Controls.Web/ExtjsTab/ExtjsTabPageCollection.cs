using System;
using System.Web;
using System.ComponentModel;
using System.Collections;
using System.Web.UI;

namespace Johnny.Controls.Web.ExtjsTab
{
    /// <summary>
    /// MenuItemCollection represents a collection of <see cref="MenuItem"/> instances.
    /// </summary>
    /// <remarks>Each item in a menu is represented by an instance of the <see cref="MenuItem"/> class.
    /// The MenuItem class has a <see cref="SubItems"/> property, which is of type MenuItemCollection.  This
    /// MenuItemCollection, then, allows for each MenuItem to have a submenu of MenuItems.<p />This flexible
    /// object model allows for an unlimited number of submenu depths.</remarks>
    public class ExtjsTabPageCollection : ICollection
    {
        #region Private Member Variables
        private ArrayList tabPages = new ArrayList();
        #endregion

        #region ICollection Implementation
        /// <summary>
        /// Adds a MenuItem to the collection.  If the ViewState is being tracked, the
        /// MenuItem's TrackViewState() method is called and the item is set to dirty, so
        /// that we don't lose any settings made prior to the Add() call.
        /// </summary>
        /// <param name="item">The MenuItem to add to the collection</param>
        /// <returns>The ordinal position of the added item.</returns>
        public virtual int Add(ExtjsTabPage item)
        {
            int result = tabPages.Add(item);

            return result;
        }

        /// <summary>
        /// Adds the MenuItems in a MenuItemCollection.
        /// </summary>
        /// <param name="items">The MenuItemCollection instance whose MenuItems to add.</param>
        public virtual void AddRange(ExtjsTabPageCollection items)
        {
            tabPages.AddRange(items);
        }

        /// <summary>
        /// Clears out the entire MenuItemCollection.
        /// </summary>
        public virtual void Clear()
        {
            tabPages.Clear();
        }

        /// <summary>
        /// Determines if a particular MenuItem exists within the collection.
        /// </summary>
        /// <param name="item">The MenuItem instance to check for.</param>
        /// <returns>A Boolean - true if the MenuItem is in the collection, false otherwise.</returns>
        public virtual bool Contains(ExtjsTabPage item)
        {
            return tabPages.Contains(item);
        }

        /// <summary>
        /// Returns the ordinal index of a MenuItem, if it exists; if the item does not exist,
        /// -1 is returned.
        /// </summary>
        /// <param name="item">The MenuItem to search for.</param>
        /// <returns>The ordinal position of the item in the collection.</returns>
        public virtual int IndexOf(ExtjsTabPage item)
        {
            return tabPages.IndexOf(item);
        }

        /// <summary>
        /// Inserts a MenuItem instance at a particular location in the collection.
        /// </summary>
        /// <param name="index">The ordinal location to insert the item.</param>
        /// <param name="item">The MenuItem to insert.</param>
        public virtual void Insert(int index, ExtjsTabPage item)
        {
            tabPages.Insert(index, item);
        }

        /// <summary>
        /// Removes a specified MenuItem from the collection.
        /// </summary>
        /// <param name="item">The MenuItem instance to remove.</param>
        public void Remove(ExtjsTabPage item)
        {
            tabPages.Remove(item);
        }

        /// <summary>
        /// Removes a MenuItem from a particular ordinal position in the collection.
        /// </summary>
        /// <param name="index">The ordinal position of the MenuItem to remove.</param>
        public void RemoveAt(int index)
        {
            tabPages.RemoveAt(index);
        }

        /// <summary>
        /// Copies the contents of the MenuItem to an array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            tabPages.CopyTo(array, index);
        }

        /// <summary>
        /// Gets an Enumerator for enumerating through the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return tabPages.GetEnumerator();
        }
        #endregion

        #region WebTabPageCollection Properties
        /// <summary>
        /// Returns the number of elements in the MenuItemCollection.
        /// </summary>
        /// <value>The actual number of elements contained in the <see cref="MenuItemCollection"/>.</value>
        [Browsable(false)]
        public virtual int Count
        {
            get
            {
                return tabPages.Count;
            }
        }


        /// <summary>
        /// Gets a value indicating whether access to the <see cref="MenuItemCollection"/> is synchronized (thread-safe).
        /// </summary>
        [Browsable(false)]
        public virtual bool IsSynchronized
        {
            get
            {
                return tabPages.IsSynchronized;
            }
        }


        /// <summary>
        /// Gets an object that can be used to synchrnoize access to the <see cref="MenuItemCollection"/>.
        /// </summary>
        [Browsable(false)]
        public virtual object SyncRoot
        {
            get
            {
                return tabPages.SyncRoot;
            }
        }


        /// <summary>
        /// Gets the <see cref="MenuItem"/> at a specified ordinal index.
        /// </summary>
        /// <remarks>Allows read-only access to the <see cref="MenuItemCollection"/>'s elements by index.
        /// For example, myMenuCollection[4] would return the fifth <see cref="MenuItem"/> instance.</remarks>
        public virtual ExtjsTabPage this[int index]
        {
            get
            {
                return (ExtjsTabPage)tabPages[index];
            }
        }

        #endregion
    }
}
