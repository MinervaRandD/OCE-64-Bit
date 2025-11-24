namespace FinishesLib
{
    using Graphics;
    using Globals;
    using System;

    public class AreaFinishLayers
    {
        private GraphicsWindow window;

        private GraphicsPage page;

        private string areaFinishName;

        public AreaFinishLayers(GraphicsWindow window, GraphicsPage page, string areaFinishName)
        {
            this.window = window;

            this.page = page;

            this.areaFinishName = areaFinishName;

            AreaDesignStateLayer = new GraphicsLayerBase(this.window, page, "[AreaMode:AreaDesignStateLayer]" + areaFinishName, GraphicsLayerType.AreaMode_AreaDesignStateLayer, GraphicsLayerStyle.Dynamic, true);
            AreaPerimeterLayer = new GraphicsLayerBase(this.window, page, "[AreaMode:AreaPerimeterLayer]" + areaFinishName, GraphicsLayerType.AreaMode_AreaPerimeterLayer, GraphicsLayerStyle.Dynamic, true);
            SeamDesignStateLayer = new GraphicsLayerBase(this.window, page, "[AreaMode:SeamDesignStateLayer]" + areaFinishName, GraphicsLayerType.AreaMode_SeamDesignStateLayer, GraphicsLayerStyle.Dynamic, true);
            RemnantSeamDesignStateLayer = new GraphicsLayerBase(this.window, page, "[AreaMode:RemnantSeamDesignStateLayer]" + areaFinishName, GraphicsLayerType.AreaMode_RemnantSeamDesignStateLayer, GraphicsLayerStyle.Dynamic,true);
            AreaFinishLabelLayer = new GraphicsLayerBase(this.window, page, "[AreaMode:AreaFinishLabelLayer]" + areaFinishName, GraphicsLayerType.AreaMode_AreaFinishLabelLayer, GraphicsLayerStyle.Dynamic, true);
            //SeamDesignStateLockLayer = new GraphicsLayerBase(this.window, page, "[AreaMode:SeamStateLockLayer]" + areaFinishName, GraphicsLayerType.AreaMode_SeamStateLockLayer, GraphicsLayerStyle.Dynamic, true);


            NormalSeamsLayer = new GraphicsLayerBase(window, page, "[NormalSeamsLayer]" + areaFinishName, GraphicsLayerType.NormalSeamsLayer, GraphicsLayerStyle.Dynamic, true);
            NormalSeamsUnhideableLayer = new GraphicsLayerBase(window, page, "[NormalSeamsUnhideableLayer]" + areaFinishName, GraphicsLayerType.NormalSeamsUnhideableLayer, GraphicsLayerStyle.Dynamic, true);
            ManualSeamsAllLayer = new GraphicsLayerBase(window, page, "[ManualSeamsAllLayer]" + areaFinishName, GraphicsLayerType.ManualSeamsAllLayer, GraphicsLayerStyle.Dynamic, true);
            CutsLayer = new GraphicsLayerBase(window, page, "[CutsLayer]" + areaFinishName, GraphicsLayerType.CutsLayer, GraphicsLayerStyle.Dynamic, true);
            OversLayer = new GraphicsLayerBase(window, page, "[OversLayer]" + areaFinishName, GraphicsLayerType.OversLayer, GraphicsLayerStyle.Dynamic, false); // Should be locked, but this is a kludge to fix an apparent error. MDD 2024-12-26
            UndrsLayer = new GraphicsLayerBase(window, page, "[UndrsLayer]" + areaFinishName, GraphicsLayerType.UndrsLayer, GraphicsLayerStyle.Dynamic, true);
            CutsIndexLayer = new GraphicsLayerBase(window, page, "[CutsIndexLayer]" + areaFinishName, GraphicsLayerType.CutsIndexLayer, GraphicsLayerStyle.Dynamic, false); // Should be locked, but this is a kludge to fix an apparent error. MDD 2024-12-26
            OversIndexLayer = new GraphicsLayerBase(window, page, "[OversIndexLayer]" + areaFinishName, GraphicsLayerType.OversIndexLayer, GraphicsLayerStyle.Dynamic, false); 
            SeamAreaIndexLayer = new GraphicsLayerBase(window, page, "[SeamAreaIndexLayer]" + areaFinishName, GraphicsLayerType.SeamAreaIndexLayer, GraphicsLayerStyle.Dynamic, false);
            UndrsIndexLayer = new GraphicsLayerBase(window, page, "[UndrsIndexLayer]" + areaFinishName, GraphicsLayerType.UndrsIndexLayer, GraphicsLayerStyle.Dynamic, true);
            EmbdCutsLayer = new GraphicsLayerBase(window, page, "[EmbdCutsLayer]" + areaFinishName, GraphicsLayerType.EmbdCutsLayer, GraphicsLayerStyle.Dynamic, true);
            EmbdOverLayer = new GraphicsLayerBase(window, page, "[EmbdOverLayer]" + areaFinishName, GraphicsLayerType.EmbdOverLayer, GraphicsLayerStyle.Dynamic, true);

        }

        public void Delete()
        {
            DeleteDesignLayers();
            DeleteSeamLayers();
        }

        public void DeleteDesignLayers()
        {
            AreaDesignStateLayer.Delete();
            AreaPerimeterLayer.Delete();
            SeamDesignStateLayer.Delete();
            RemnantSeamDesignStateLayer.Delete();
            AreaFinishLabelLayer.Delete();
           // SeamDesignStateLockLayer.Delete();
        }

        public void DeleteSeamLayers()
        {
            NormalSeamsLayer.Delete();
            NormalSeamsUnhideableLayer.Delete();
            ManualSeamsAllLayer.Delete();
            CutsLayer.Delete();
            OversLayer.Delete();
            UndrsLayer.Delete();
            CutsIndexLayer.Delete();
            OversIndexLayer.Delete();
            UndrsIndexLayer.Delete();
            EmbdCutsLayer.Delete();
            EmbdOverLayer.Delete();
        }

        #region Design State Layers

        public GraphicsLayerBase AreaDesignStateLayer { get; private set; }

        public GraphicsLayerBase AreaPerimeterLayer { get; private set; }

        public GraphicsLayerBase SeamDesignStateLayer { get; private set; }

        public GraphicsLayerBase RemnantSeamDesignStateLayer { get; private set; }

        public GraphicsLayerBase AreaFinishLabelLayer { get; private set; }

        //public GraphicsLayerBase SeamDesignStateLockLayer { get; private set; }

        #endregion

        #region Seaming Related Layers

        public GraphicsLayerBase NormalSeamsLayer { get; private set; }

        public GraphicsLayerBase NormalSeamsUnhideableLayer { get; private set; }

        public GraphicsLayerBase ManualSeamsAllLayer { get; private set; }

        public GraphicsLayerBase CutsLayer { get; private set; }

        public GraphicsLayerBase OversLayer { get; private set; }

        public GraphicsLayerBase UndrsLayer { get; private set; }

        public GraphicsLayerBase CutsIndexLayer { get; private set; }

        public GraphicsLayerBase OversIndexLayer { get; private set; }

        public GraphicsLayerBase UndrsIndexLayer { get; private set; }

        public GraphicsLayerBase EmbdCutsLayer { get; private set; }

        public GraphicsLayerBase EmbdOverLayer { get; private set; }

        public GraphicsLayerBase SeamAreaIndexLayer { get; private set; }
        #endregion

        private void deleteSeamLayer(ref GraphicsLayer layer)
        {
            if (layer is null)
            {
                return;
            }

            page.RemoveFromGraphicsLayerDict(layer);

            layer.Delete();

            layer = null;
        }

       
        

        public void SetupAllSeamLayers(
            bool inAreaDesignState
            , bool selected
            , bool filtered
            , bool showNormalSeams
            , bool showNormalSeamsUnhideable
            , bool showManualSeamsAll
            , bool showCuts
            , bool showCutIndices
            , bool showOvers
            , bool showUndrs
            , bool showEmbdCuts
            , bool showEmbdOvers
            , bool showSeamAreaNmbrs)
        {
            bool isSelected = false;

            if (inAreaDesignState)
            {
                isSelected = true;
            }

            else
            {

                isSelected = selected;
            }

            bool showAllSeams = SystemState.CkbShowAllSeamsInSeamMode.Checked;

            NormalSeamsLayer.SetLayerVisibility((showAllSeams || isSelected) && showNormalSeams && !filtered);
            NormalSeamsUnhideableLayer.SetLayerVisibility((showAllSeams || isSelected) && (showNormalSeams || showNormalSeamsUnhideable) && !filtered);
            ManualSeamsAllLayer.SetLayerVisibility((showAllSeams || isSelected) && showManualSeamsAll && !filtered);
            CutsLayer.SetLayerVisibility(isSelected && showCuts && !filtered);
            CutsIndexLayer.SetLayerVisibility(isSelected && showCutIndices && !filtered); // Bernie, check here.
            OversLayer.SetLayerVisibility(isSelected && showOvers && !filtered);
            OversIndexLayer.SetLayerVisibility(isSelected && showOvers && !filtered);
            SeamAreaIndexLayer.SetLayerVisibility(isSelected && showSeamAreaNmbrs && !filtered);
            UndrsLayer.SetLayerVisibility(isSelected && showUndrs && !filtered);
            UndrsIndexLayer.SetLayerVisibility(isSelected && showUndrs && !filtered);
            EmbdCutsLayer.SetLayerVisibility(isSelected && showEmbdCuts && !filtered);
            EmbdOverLayer.SetLayerVisibility(isSelected && showEmbdOvers && !filtered);
        }

    }
}
