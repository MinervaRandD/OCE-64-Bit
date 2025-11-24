using FinishesLib;
using CanvasManagerLib;
using Graphics;
using System;
using System.Collections.Generic;
using TracerLib;
using Globals;

namespace CanvasManagerLib
{
    public class SeamFinishManager : IDisposable
    {
        public string Guid;

        public SeamFinishBaseList SeamFinishBaseList { get; set; }

        public SeamFinishBase SeamFinishBase { get; set; }
       
        //--------------------------------------------------------------------------------------------------------------//
        //                          Seam dictionary elements                                                            //
        //--------------------------------------------------------------------------------------------------------------//

        public Dictionary<string, CanvasSeam> SeamDict
        {
            get;
            set;
        } = new Dictionary<string, CanvasSeam>();

        public IEnumerable<CanvasSeam> CanvasDirectedSeams => SeamDict.Values;

        public bool SeamDictContains(string guid) => SeamDict.ContainsKey(guid);

        public bool SeamDictContains(CanvasSeam seam) => SeamDictContains(seam.Guid);

        private bool SeamDictContains(object guid)
        {
            throw new NotImplementedException();
        }

        public void ClearSeamDict() => SeamDict.Clear();

        public void AddToSeamDict(CanvasSeam seam)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { seam });
#endif
            if (SeamDict.ContainsKey(seam.Guid))
            {
                Tracer.TraceGen.TraceError("Attempt to add a seam to SeamDict in UCSeamFinishPaletteElement:AddToSeamDict that already exists in the dictionary", 1, true);
                return;
            }

            SeamDict.Add(seam.Guid, seam);
        }

        public void RemoveFromSeamDict(CanvasSeam seam)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { seam });
#endif

            RemoveFromSeamDict(seam.Guid);
        }

        public void RemoveFromSeamDict(string guid)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { guid });
#endif

            if (!SeamDict.ContainsKey(guid))
            {
                Tracer.TraceGen.TraceError("Attempt to remove a seam from SeamDict in UCSeamFinishPaletteElement:RemoveFromSeamDict that does not exist in the dictionary", 1, true);
                return;
            }

            SeamDict.Remove(guid);
        }

        public int SeamDictCount => SeamDict.Count;

        public CanvasSeam GetSeamFromSeamDict(string guid)
        {
            if (SeamDictContains(guid))
            {
                return SeamDict[guid];
            }

            return null;
        }

        //--------------------------------------------------------------------------------------------------------------//
        public SeamFinishLayers SeamFinishLayers { get; set; }

        #region Layers


        //------------------------------------------------------------------------------------------------//
        // In the following, the layers are created on demand so as to reduce the total number of layers. //
        // It appears that too many layers slow the visio rendering process                               //
        //------------------------------------------------------------------------------------------------//

        public GraphicsLayerBase AreaDesignStateLayer => SeamFinishLayers.AreaDesignStateLayer;

        public GraphicsLayerBase LineDesignStateLayer => SeamFinishLayers.LineDesignStateLayer;

        public GraphicsLayerBase SeamDesignStateLayer => SeamFinishLayers.SeamDesignStateLayer;

        public bool Filtered => SeamFinishBase.Filtered;

        public bool Selected => SeamFinishBase.Selected;

        #endregion

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }


        public Dictionary<string, CanvasDirectedLine> CanvasDirectedLineDict = new Dictionary<string, CanvasDirectedLine>();

        public SeamFinishManager(GraphicsWindow window, GraphicsPage page, SeamFinishBaseList seamFinishBaseList, SeamFinishBase seamFinishBase)
        {
            this.SeamFinishBaseList = seamFinishBaseList;

            this.SeamFinishBase = seamFinishBase;

            this.Guid = SeamFinishBase.Guid;

            this.Window = window;

            this.Page = page;

            SeamFinishLayers = new SeamFinishLayers(window, page, seamFinishBase.SeamName);
        }

        public void SetSeamState(DesignState designState, SeamMode seamMode, bool selected)
        {
            if (SeamDictCount <= 0)
            {
                return;
            }

            if (Filtered)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                LineDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);

                return;
            }


            if (designState == DesignState.Area)
            {
                AreaDesignStateLayer.SetLayerVisibility(true);
                LineDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);
            }

            else if (designState == DesignState.Line)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                LineDesignStateLayer.SetLayerVisibility(true);
                SeamDesignStateLayer.SetLayerVisibility(false);
            }

            else if (designState == DesignState.Seam)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                LineDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(true);
            }
        }

        public void Delete()
        {
            DeleteLayers();
        }

        public void Dispose()
        {
            DeleteLayers();
        }

        public void DeleteLayers()
        {
            AreaDesignStateLayer.Delete();
            LineDesignStateLayer.Delete();
            SeamDesignStateLayer.Delete();
        }
    }
}
