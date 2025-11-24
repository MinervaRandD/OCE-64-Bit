using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class CoordinatedList<Type> : List<Type>
    {
        public delegate void ItemAddedEvent(Type item);

        public event ItemAddedEvent ItemAdded;

        public delegate void ItemRemovedEvent(Type item);

        public event ItemRemovedEvent ItemRemoved;

        public delegate void ListClearedEvent();

        public event ListClearedEvent ListCleared;

        public CoordinatedList() { }

        public List<Type> ToList()
        {
            return (List<Type>) this;
        }

        public new void Add(Type item)
        {
            base.Add(item);

            if (ItemAdded != null)
            {
                ItemAdded.Invoke(item);
            }
        }

        public new void Remove(Type item)
        {
            base.Remove(item);

            if (ItemRemoved != null)
            {
                ItemRemoved.Invoke(item);
            }
        }

        public new void Clear()
        {
            base.Clear();

            if (ListCleared != null)
            {
                ListCleared.Invoke();
            }
        }

        public virtual void AddBase(Type item)
        {
            base.Add(item);
        }

        public virtual void RemoveBase(Type item)
        {
            base.Remove(item);
        }

        public virtual void ClearBase()
        {
            base.Clear();
        }
    }
}
