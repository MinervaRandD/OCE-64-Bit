using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class CoordinatedList2 : List<ICoordinatedListElement>
    {
        public delegate void ItemAddedEvent(ICoordinatedListElement item);

        public event ItemAddedEvent ItemAdded;

        public delegate void ItemRemovedEvent(ICoordinatedListElement item);

        public event ItemRemovedEvent ItemRemoved;

        public delegate void ListClearedEvent();

        public event ListClearedEvent ListCleared;

        public CoordinatedList2 SubList = null;


        public CoordinatedList2()
        {

        }

        public CoordinatedList2(CoordinatedList2 subList)
        {
            SubList = subList;
        }

        public new void Add(ICoordinatedListElement item)
        {
            base.Add(item);

            if (SubList != null)
            {
                ICoordinatedListElement subItem = item.GetSubElement;

                if (subItem != null)
                {
                    SubList.Add(subItem);
                }
            }

            if (ItemAdded != null)
            {
                ItemAdded.Invoke(item);
            }
        }

        public new void Remove(ICoordinatedListElement item)
        {
            base.Remove(item);

            if (SubList != null)
            {
                ICoordinatedListElement subItem = item.GetSubElement;

                if (subItem != null)
                {
                    SubList.Remove(subItem);
                }
            }

            if (ItemRemoved != null)
            {
                ItemRemoved.Invoke(item);
            }
        }

        public new void Clear()
        {
            base.Clear();

            if (SubList != null)
            {
                SubList.Clear();
            }

            if (ListCleared != null)
            {
                ListCleared.Invoke();
            }
        }
    }
}
