namespace CanvasLib.Counters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Supporting_Forms;
    using Geometry;
    using Graphics;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CanvasManager
    {
        public CounterList Counters;

        public bool CountersActivated = false;

        private wnfCounters countersForm = null;

        public int SelectedCounterIndex = 0;

        public void ActivateCounters()
        {
            if (CountersActivated)
            {
                return;
            }

            BaseForm.btnCounters.Checked = true;

            BaseForm.cccAreaMode.Activate(this.SelectedCounterIndex);
            BaseForm.cccLineMode.Activate(this.SelectedCounterIndex);

            CountersActivated = true;

            if (countersForm == null)
            {
                countersForm = new wnfCounters(this, Counters);

                countersForm.Show(BaseForm);

                countersForm.BringToFront();
            }

        }

        private void deactivateCounters()
        {
            if (!CountersActivated)
            {
                return;
            }

            if (countersForm != null)
            {
                countersForm.Close(false);

                countersForm = null;
            }

            BaseForm.btnCounters.Checked = false;

            BaseForm.cccAreaMode.Deactivate();
            BaseForm.cccLineMode.Deactivate();

            CountersActivated = false;
        }

        public void ToggleCountersActivation()
        {
            if (CountersActivated)
            {
                deactivateCounters();
            }

            else
            {
                ActivateCounters();
            }
        }

        public void SelectRow(int counterIndex)
        {
            SelectedCounterIndex = counterIndex;

            BaseForm.cccAreaMode.SelectCounter(counterIndex);
            BaseForm.cccLineMode.SelectCounter(counterIndex);
        }

        public void IncrementCounterCount(int counterIndex)
        {
            Counter counter = Counters[counterIndex];

            counter.Count++;

            if (countersForm != null)
            {
                countersForm.UpdateCount(counterIndex);
            }

            BaseForm.cccAreaMode.UpdateCount(counterIndex);
            BaseForm.cccLineMode.UpdateCount(counterIndex);
        }

        public void IncrementCounterCount(GraphicsCounter graphicsCounter)
        {
            int counterIndex = graphicsCounter.CounterIndex;

            IncrementCounterCount(counterIndex);
        }

        public void DecrementCounterCount(int counterIndex)
        {
            Counter counter = Counters[counterIndex];

            counter.Count = Math.Max(0, counter.Count - 1);

            if (countersForm != null)
            {
                countersForm.UpdateCount(counterIndex);
            }

            BaseForm.cccAreaMode.UpdateCount(counterIndex);
            BaseForm.cccLineMode.UpdateCount(counterIndex);
        }

        public void SetCounterCount(int counterIndex, int count)
        {
            Counter counter = Counters[counterIndex];

            counter.Count = count;

            if (countersForm != null)
            {
                countersForm.SetCount(counterIndex, 0);
            }

            BaseForm.cccAreaMode.SetCount(counterIndex, 0);
            BaseForm.cccLineMode.SetCount(counterIndex, 0);
        }

        internal void UpdateSelectedCounterDescription()
        {
            if (countersForm != null)
            {
                countersForm.UpdateSelectedCounterDescription();
            }

            BaseForm.cccAreaMode.UpdateSelectedCounterDescription();
            BaseForm.cccLineMode.UpdateSelectedCounterDescription();
        }

        internal void UpdatedCounterColors()
        {
            foreach (GraphicsCounter graphicsCounter in CurrentPage.GraphicsCounters)
            {
                Counter counter = Counters[graphicsCounter.CounterTag - 'A'];

                graphicsCounter.UpdateColor(counter.Color);
            }
        }

        private void processCounterModeClick(double x, double y)
        {
            GraphicsCounter graphicsCounter = CurrentPage.GetSelectedCounter(x, y);

            if (!(graphicsCounter is null))
            {
                string guid = graphicsCounter.Guid;

                GraphicsCounterDict.Remove(guid);

                DecrementCounterCount((int)(graphicsCounter.CounterTag - 'A'));

                CurrentPage.RemoveCounterFromLayer(graphicsCounter);

                graphicsCounter.Delete();
            }

            else
            {
                Counter counter = Counters[SelectedCounterIndex];

                IncrementCounterCount(SelectedCounterIndex);

                graphicsCounter = new GraphicsCounter(
                    CurrentPage, new Coordinate(x, y), .14, counter.Color, counter.Tag);

                graphicsCounter.Draw("12");

                VsoWindow.DeselectAll();

                GraphicsCounterDict.Add(graphicsCounter.Guid, graphicsCounter);

                CurrentPage.AddCounterToLayer(graphicsCounter);
            }
        }

        internal void SetCounterVisibility(int counterIndex, bool visible)
        {
            CurrentPage.SetCounterVisibility(counterIndex, visible);
        }

        internal void RemoveCounter(int counterIndex)
        {
            if (Counters[counterIndex].Count == 0)
            {
                return;
            }

            List<string> countersToRemove = new List<string>();

            foreach (GraphicsCounter graphicsCounter in this.GraphicsCounterDict.Values)
            {
                if (graphicsCounter.CounterTag - 'A' == counterIndex)
                {
                    countersToRemove.Add(graphicsCounter.NameID);

                    CurrentPage.RemoveCounterFromLayer(graphicsCounter);

                    graphicsCounter.Shape.Delete();
                }
            }

            countersToRemove.ForEach(s => GraphicsCounterDict.Remove(s));

            SetCounterCount(counterIndex, 0);
        }

    }
}
