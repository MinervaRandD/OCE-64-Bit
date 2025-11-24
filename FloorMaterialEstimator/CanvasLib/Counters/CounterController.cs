namespace CanvasLib.Counters
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Geometry;
    using Graphics;
    using SettingsLib;
    using Utilities;
    using Globals;

    using Visio = Microsoft.Office.Interop.Visio;

    public class CounterController
    {
        public delegate void DebugUpdateRequiredHandler();

        public event DebugUpdateRequiredHandler DebugUpdateRequired;

        public CounterList CounterList;

        public bool CountersActivated = false;

        public wnfCounters CountersForm = null;

        public int SelectedCounterIndex = -1;

        public Dictionary<string, GraphicsCounter>[] GraphicsCounterDict { get; set; } = new Dictionary<string, GraphicsCounter>[CounterList.CountersSize];

        public GraphicsLayer[] CounterLayer = new GraphicsLayer[CounterList.CountersSize];

        public bool[] LayerVisibility = new bool[CounterList.CountersSize];

        public IEnumerable<GraphicsCounter> GraphicsCounters(int i) => GraphicsCounterDict[i].Values;

        private GraphicsPage page;

        private GraphicsWindow window;

        private Visio.Window vsoWindow;

        private CounterControl cccAreaMode;

        private CounterControl cccLineMode;

        private ToolStripButton btnCounters;

        public string CountersFilePath;

        public IBaseForm IBaseForm;

        public CounterController(
            IBaseForm iBaseForm
            , GraphicsWindow window
            , GraphicsPage page
            , CounterControl cccAreaMode
            , CounterControl cccLineMode
            , ToolStripButton btnCounters
            , string countersFilePath)
        {
            this.IBaseForm = iBaseForm;

            this.window = window;

            this.page = page;

            this.cccAreaMode = cccAreaMode;
            this.cccLineMode = cccLineMode;
            this.btnCounters = btnCounters;
            this.CountersFilePath = countersFilePath;
            
            this.vsoWindow = window.VisioWindow;

            this.cccAreaMode.Init(this);
            this.cccLineMode.Init(this);

            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                GraphicsCounterDict[i] = new Dictionary<string, GraphicsCounter>();
            }

            CounterList = new CounterList();

            CounterList.Init(countersFilePath);

            foreach (Counter counter in CounterList.Counters)
            {
                counter.CounterColorChanged += Counter_CounterColorChanged;
                counter.CounterDisplaySizeChanged += Counter_CounterDisplaySizeChanged;
                counter.CounterShowCircleChanged += Counter_CounterShowCircleChanged;
            }

            GlobalSettings.CounterSmallCircleRadiusChanged += GlobalSettings_CounterSmallCircleRadiusChanged;
            GlobalSettings.CounterMediumCircleRadiusChanged += GlobalSettings_CounterMediumCircleRadiusChanged;
            GlobalSettings.CounterLargeCircleRadiusChanged += GlobalSettings_CounterLargeCircleRadiusChanged;

            GlobalSettings.CounterSmallFontSizeChanged += GlobalSettings_CounterSmallFontSizeChanged;
            GlobalSettings.CounterMediumFontSizeChanged += GlobalSettings_CounterMediumFontSizeChanged;
            GlobalSettings.CounterLargeFontSizeChanged += GlobalSettings_CounterLargeFontSizeChanged;

            IBaseForm.BtnLineModeCounterActivate = this.cccLineMode.BtnActivate;
            IBaseForm.BtnLineModeCounterActivate_Click = this.cccLineMode.BtnActivate_Click;
        }

        internal void UnshowAllCounters()
         {
            foreach (Counter counter in this.CounterList.Counters)
            {
                counter.Filtered = true;
            }
        }

        public void SetAllCounterLayersToInvisibile()
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                if (CounterLayer[i] == null)
                {
                    continue ;
                }

                CounterLayer[i].SetLayerVisibility(false);
            }
        }

        public void ResetAllCounterLayerVisibility()
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                if (CounterLayer[i] == null)
                {
                    continue ;
                }

                CounterLayer[i].SetLayerVisibility(LayerVisibility[i]);
            }
        }

        public void DeleteLayers()
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                if (CounterLayer[i] is null)
                {
                    continue;
                }

                page.RemoveFromGraphicsLayerDict(CounterLayer[i]);

                CounterLayer[i].RemoveAllShapes();

                CounterLayer[i].Delete();

                CounterLayer[i] = null;

                LayerVisibility[i] = false;
            }
        }

        public void UpdateCountersList(CounterList counterList)
        {
            CounterList.UpdateCountersList(counterList);
        }

        private void GlobalSettings_CounterSmallCircleRadiusChanged(double radius)
        {
            foreach (Counter counter in CounterList.Counters)
            {
                if (counter.CounterDisplaySize == CounterDisplaySize.Small)
                {
                    foreach (GraphicsCounter graphicsCounter in GraphicsCounters(counter.CounterIndex))
                    {
                        graphicsCounter.Refresh();
                    }
                }
            }

            window?.DeselectAll();
 
        }

        private void GlobalSettings_CounterMediumCircleRadiusChanged(double radius)
        {
            foreach (Counter counter in CounterList.Counters)
            {
                if (counter.CounterDisplaySize == CounterDisplaySize.Medium)
                {
                    foreach (GraphicsCounter graphicsCounter in GraphicsCounters(counter.CounterIndex))
                    {
                        graphicsCounter.Refresh();
                    }
                }
            }

            window?.DeselectAll();
        }

        private void GlobalSettings_CounterLargeCircleRadiusChanged(double radius)
        {
            foreach (Counter counter in CounterList.Counters)
            {
                if (counter.CounterDisplaySize == CounterDisplaySize.Large)
                {
                    foreach (GraphicsCounter graphicsCounter in GraphicsCounters(counter.CounterIndex))
                    {
                        graphicsCounter.Refresh();
                    }
                }
            }

            window?.DeselectAll();
        }

        private void GlobalSettings_CounterSmallFontSizeChanged(double fontSize)
        {
            foreach (Counter counter in CounterList.Counters)
            {
                if (counter.CounterDisplaySize == CounterDisplaySize.Small)
                {
                    foreach (GraphicsCounter graphicsCounter in GraphicsCounters(counter.CounterIndex))
                    {
                        graphicsCounter.UpdateText();
                    }
                }
            }

            window?.DeselectAll();
        }

        private void GlobalSettings_CounterMediumFontSizeChanged(double fontSize)
        {
            foreach (Counter counter in CounterList.Counters)
            {
                if (counter.CounterDisplaySize == CounterDisplaySize.Medium)
                {
                    foreach (GraphicsCounter graphicsCounter in GraphicsCounters(counter.CounterIndex))
                    {
                        graphicsCounter.UpdateText();
                    }
                }
            }

            window?.DeselectAll();
        }

        private void GlobalSettings_CounterLargeFontSizeChanged(double fontSize)
        {
            foreach (Counter counter in CounterList.Counters)
            {
                if (counter.CounterDisplaySize == CounterDisplaySize.Large)
                {
                    foreach (GraphicsCounter graphicsCounter in GraphicsCounters(counter.CounterIndex))
                    {
                        graphicsCounter.UpdateText();
                    }
                }
            }

            window?.DeselectAll();
        }

        private void Counter_CounterColorChanged(Counter counter, System.Drawing.Color color)
        {
            int indx = counter.Tag - 'A';

            foreach (GraphicsCounter graphicsCounter in GraphicsCounters(indx))
            {
                graphicsCounter.UpdateColor(counter.Color);
            }
        }

        private void Counter_CounterDisplaySizeChanged(Counter counter, CounterDisplaySize counterSize)
        {
            int indx = counter.Tag - 'A';

            foreach (GraphicsCounter graphicsCounter in GraphicsCounters(indx))
            {
                graphicsCounter.UpdateSize(counterSize);
            }

            window?.DeselectAll();
        }

        private void Counter_CounterShowCircleChanged(Counter counter, bool showCircle)
        {
            int indx = counter.Tag - 'A';

            foreach (GraphicsCounter graphicsCounter in GraphicsCounters(indx))
            {
                graphicsCounter.UpdateShowCircle(showCircle);
            }
        }

        public void ActivateCounters()
        {
            if (CountersActivated)
            {
                return;
            }

            if (IBaseForm.BtnDoorTakeoutActivate.Text == "Deactivate")
            {
                IBaseForm.BtnDoorTakeoutActivate_Click("Activate Counters", null);
            }
            
            btnCounters.Checked = true;

            //this.BtnActivate.Text = "Deactivate";

            cccAreaMode.Activate(this.SelectedCounterIndex);
            cccLineMode.Activate(this.SelectedCounterIndex);

            CountersActivated = true;



            if (CountersForm == null)
            {
                CountersForm = new wnfCounters(this, CounterList);

                CountersForm.Show();
            }

        }

        public void DeactivateCounters()
        {
            if (!CountersActivated)
            {
                return;
            }

            if (CountersForm != null)
            {
                CountersForm.Close(false);

                CountersForm = null;
            }

            btnCounters.Checked = false;

            cccAreaMode.Deactivate();
            cccLineMode.Deactivate();

            CountersActivated = false;
        }

        public void ToggleCountersActivation()
        {
            if (CountersActivated)
            {
                DeactivateCounters();
            }

            else
            {
                ActivateCounters();
            }
        }

        public void SelectRow(int counterIndex)
        {
            if (counterIndex < 0 || counterIndex >= CounterList.Counters.Length)
            {
                return;
            }

            SelectedCounterIndex = counterIndex;

            cccAreaMode.SelectCounter(counterIndex);
            cccLineMode.SelectCounter(counterIndex);
        }

        public void IncrementCounterCount(int counterIndex)
        {

            Counter counter = CounterList[counterIndex];

            counter.Count++;

            if (CountersForm != null)
            {
                CountersForm.UpdateCount(counterIndex);
            }

            cccAreaMode.UpdateCountTotal(counterIndex);
            cccLineMode.UpdateCountTotal(counterIndex);
        }

        public void IncrementCounterCount(GraphicsCounter graphicsCounter)
        {
            int counterIndex = graphicsCounter.CounterIndex;

            IncrementCounterCount(counterIndex);
        }

        public void DecrementCounterCount(int counterIndex)
        {
            Counter counter = CounterList[counterIndex];

            counter.Count = Math.Max(0, counter.Count - 1);

            if (CountersForm != null)
            {
                CountersForm.UpdateCount(counterIndex);
            }

            cccAreaMode.UpdateCountTotal(counterIndex);
            cccLineMode.UpdateCountTotal(counterIndex);
        }

        public void SetCounterCount(int counterIndex, int count)
        {
            Counter counter = CounterList[counterIndex];

            counter.Count = count;

            //if (CountersForm != null)
            //{
            //    CountersForm.SetCount(counterIndex, 0);
            //}

            cccAreaMode.SetCount(counterIndex, 0);
            cccLineMode.SetCount(counterIndex, 0);
        }

        public void CounterSizeChanged()
        {
            var counterIndex = this.SelectedCounterIndex;

            cccAreaMode.UpdateCountTotal(counterIndex);
            cccLineMode.UpdateCountTotal(counterIndex);
        }

        internal void UpdateSelectedCounterDescription()
        {
            if (CountersForm != null)
            {
                CountersForm.UpdateSelectedCounterDescription();
            }

            cccAreaMode.UpdateSelectedCounterDescription();
            cccLineMode.UpdateSelectedCounterDescription();
        }

        internal void UpdatedCounterColors()
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                foreach (GraphicsCounter graphicsCounter in GraphicsCounters(i))
                {
                    Counter counter = CounterList[graphicsCounter.CounterTag - 'A'];

                    graphicsCounter.UpdateColor(counter.Color);
                }
            }
        }

        public void AddCountersToPalette(List<GraphicsCounter> graphicsCounterList)
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                foreach (GraphicsCounter graphicsCounter in graphicsCounterList)
                {
                    if (i == graphicsCounter.CounterIndex)
                    {
                        GuidMaintenance.AddGuid(graphicsCounter.Guid, graphicsCounter);

                        Counter counter = CounterList[graphicsCounter.CounterIndex];

                        counter.Count++;

                        graphicsCounter.Draw();

                        window?.DeselectAll();

                        GraphicsCounterDict[i].Add(graphicsCounter.Guid, graphicsCounter);

                        AddCounterToLayer(graphicsCounter);

                        page.AddToPageShapeDict(graphicsCounter);
                    }
                }
            }
        }

        public void ProcessCounterModeClick(double x, double y)
        {
            GraphicsCounter graphicsCounter = GetSelectedCounter(x, y);

            if (!(graphicsCounter is null))
            {
                int indx = graphicsCounter.CounterIndex;

                string guid = graphicsCounter.Guid;

                GraphicsCounterDict[indx].Remove(guid);

                DecrementCounterCount(indx);

                RemoveCounterFromLayer(graphicsCounter);

                graphicsCounter.Delete();
            }

            else
            {
                if (SelectedCounterIndex < 0)
                {
                    MessageBoxAdv.Show("Please select a counter", "No Counter Selected", MessageBoxAdv.Buttons.OK);
                    return;
                }

                Counter counter = CounterList[SelectedCounterIndex];

                IncrementCounterCount(SelectedCounterIndex);

                graphicsCounter = new GraphicsCounter(
                    window, page, new Coordinate(x, y), counter);

                graphicsCounter.Draw();

                window?.DeselectAll();

                GraphicsCounterDict[SelectedCounterIndex].Add(graphicsCounter.Guid, graphicsCounter);

                AddCounterToLayer(graphicsCounter);
            }

           
        }

        internal void RemoveAllCounters()
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                RemoveCounter(i);
            }
        }
        
        internal void RemoveCounter(int counterIndex)
        {
            if (CounterList[counterIndex].Count == 0)
            {
                return;
            }

            foreach (GraphicsCounter graphicsCounter in this.GraphicsCounters(counterIndex))
            {
                RemoveCounterFromLayer(graphicsCounter);

                graphicsCounter.Shape.Delete();
            }

            GraphicsCounterDict[counterIndex].Clear();

            SetCounterCount(counterIndex, 0);
        }

        public GraphicsCounter GetSelectedCounter(double x, double y)
        {
            Visio.Selection selection = page.VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection.Count > 0)
            {
                foreach (Visio.Shape visioShape in selection)
                {
                    string guid = visioShape.Data3;

                    for (int i = 0; i < CounterList.CountersSize; i++)
                    {
                        if (GraphicsCounterDict[i].ContainsKey(guid))
                        {
                            return GraphicsCounterDict[i][guid];
                        }
                    }
                }
            }

            return null;
        }

        public void ResetCounters()
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                CounterList[i].Count = 0;

                foreach (GraphicsCounter graphicsCounter in GraphicsCounters(i))
                {
                    graphicsCounter.Delete();
                }
            }

            CounterList.ResetCounters();


            if (CountersForm != null)
            {
                CountersForm.ResetCounters();
            }
        }

        public void RemoveCounters()
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                GraphicsCounterDict[i].Clear();

                if (Utilities.IsNotNull(CounterLayer[i]))
                {
                    page.RemoveFromGraphicsLayerDict(CounterLayer[i]);

                    CounterLayer[i].Delete();

                    CounterLayer[i] = null;

                    LayerVisibility[i] = false;
                }
            }
        }

        public void AddCounterToLayer(GraphicsCounter graphicsCounter)
        {
            int counterIndex = graphicsCounter.CounterTag - 'A';

            if (CounterLayer[counterIndex] is null)
            {

                CounterLayer[counterIndex] = new GraphicsLayer(null, window, page, "[CounterController]Counter-" + graphicsCounter.CounterTag, GraphicsLayerType.Counter, GraphicsLayerStyle.Dynamic);
                
                CounterLayer[counterIndex].Lock();

                page.AddToGraphicsLayerDict(CounterLayer[counterIndex]);

                if (CounterList[counterIndex] != null)
                {
                    LayerVisibility[counterIndex] = CounterList[counterIndex].Show;
                    CounterLayer[counterIndex].SetLayerVisibility(LayerVisibility[counterIndex]);
                }

                else
                {
                    LayerVisibility[counterIndex] = true;
                    CounterLayer[counterIndex].SetLayerVisibility(LayerVisibility[counterIndex]);
                }
            }

            CounterLayer[counterIndex].AddShape(graphicsCounter.Shape, 1);
        }

        public void RemoveCounterFromLayer(GraphicsCounter graphicsCounter, bool removeLayer = false)
        {
            int counterIndex = graphicsCounter.CounterTag - 'A';

            if (CounterLayer[counterIndex] is null)
            {
                return;
            }

            CounterLayer[counterIndex].RemoveShape(graphicsCounter.Shape);

            VisioInterop.DeleteShape(graphicsCounter.Shape);

            if (removeLayer)
            {
                page.RemoveFromGraphicsLayerDict(CounterLayer[counterIndex]);

                CounterLayer[counterIndex].Delete();

                CounterLayer[counterIndex] = null;

                LayerVisibility[counterIndex] = false;
            }
        }

        public void SetCounterVisibility(int counterIndex, bool visible)
        {
            if (CounterLayer[counterIndex] == null)
            {
                return;
            }

            CounterLayer[counterIndex].SetLayerVisibility(visible);

            LayerVisibility[counterIndex] = visible;
        }

        //public void SetupCounterLayers()
        //{
        //    for (int i = 0; i < CounterList.CountersSize; i++)
        //    {
        //        char tag = (char)('A' + i);

        //        CounterLayer[i] = new GraphicsLayer(Window, Page, "[CounterController]Counter-" + tag, GraphicsLayerType.Dynamic);
        //    }

        //}

        public void SelectUpdateCounterSize(string oldDescription, string newDescription, double oldSizeInches, double newSizeInches)
        {
            Counter counter = CounterList.FindByDescriptionSize(oldDescription, oldSizeInches);

            if (counter != null)
            {
                if (counter.Description != newDescription)
                {
                    counter.Description = newDescription;
                }
                counter.Size = newSizeInches / 12.0;
            }
        }

        internal void SetAllCountersSize(CounterDisplaySize counterSize)
        {
            foreach (Counter counter in CounterList.Counters)
            {
                counter.CounterDisplaySize = counterSize;
            }
        }
    }
}
