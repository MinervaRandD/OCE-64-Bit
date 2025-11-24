using FinishesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasLib.Filters.Area_Filter
{
    public class AreaFilters
    {
        public AreaFinishBaseList AreaFinishList { get; set; }

        public delegate void SeamFilterChangedHandler(AreaFilters filter);

        public event SeamFilterChangedHandler SeamFilterChanged;

        public SeamFilter SeamFilter { 

            get 
            {
                return this.seamFilter;
            } 

            set
            {
                if (this.seamFilter != value)
                {
                    this.seamFilter = value;

                    NotifySeamFilterChanged();
                }
            } 
        }

        public AreaFilters(AreaFinishBaseList areaFinishList)
        {
            this.AreaFinishList = areaFinishList;
            this.seamFilter =  SeamFilter.Show;
        }

        public bool ItemsFiltered()
        {
            return AreaFinishList.ItemsFiltered();
        }

        private void NotifySeamFilterChanged()
        {
            if (this.SeamFilterChanged != null)
            {
                this.SeamFilterChanged(this);
            }
        }

        private SeamFilter seamFilter;
    }
}
