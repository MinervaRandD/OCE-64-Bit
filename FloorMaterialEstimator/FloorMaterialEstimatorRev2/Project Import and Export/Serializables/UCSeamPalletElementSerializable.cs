

namespace FloorMaterialEstimator
{
    using PaletteLib;

    public class UCSeamPalletElementSerializable
    {
        public string SeamFinishBaseGuid { get; set; }

        public string Product { get; set; }
        public string Notes { get; set; }

        public UCSeamPalletElementSerializable() { }

        public UCSeamPalletElementSerializable(UCSeamPaletteElement ucSeamPalletElement)
        {
            this.SeamFinishBaseGuid = ucSeamPalletElement.SeamFinishBase.Guid;

            this.Product = ucSeamPalletElement.Product;
            this.Notes = ucSeamPalletElement.Notes;
        }
    }
}
