using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities.Supporting_Controls
{
    public class UCFlowLayoutPanel<T> : FlowLayoutPanel, IEnumerable<T>
        where T : Control
    {
        public T this[int index]
        {
            get
            {
                return (T)base.Controls[index];
            }
        }

        public int Count => base.Controls.Count;


        public IEnumerator<T> GetEnumerator()
        {
            return new FlowPanelEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FlowPanelEnumerator<T>(this);
        }

        public void Add(T ucFinish) => Controls.Add(ucFinish); 

        public int GetChildIndex(T ucFinish) => Controls.GetChildIndex(ucFinish);

        public void SetChildIndex(T ucFinish, int position) => Controls.SetChildIndex(ucFinish, position);

        public void RemoveAt(int position) => Controls.RemoveAt(position);

    }


    public class FlowPanelEnumerator<T> : IEnumerator<T>
        where T: Control
    {
        UCFlowLayoutPanel<T> flowPanel;

        public FlowPanelEnumerator(UCFlowLayoutPanel<T> flowPanel)
        {
            this.flowPanel = flowPanel;
        }

        int index = -1;

        public T Current => flowPanel[index];

        object IEnumerator.Current => flowPanel[index];

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (index >= flowPanel.Count - 1)
            {
                return false;
            }

            index++;

            return true;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
