
using System.Collections.Generic;
using PaletteLib;

namespace FloorMaterialEstimator
{
    public class UCSeamPalletSerializable
    {
        public List<UCSeamPalletElementSerializable> SeamPalletElementList = new List<UCSeamPalletElementSerializable>();

        public UCSeamPalletSerializable() { }

        public UCSeamPalletSerializable(UCSeamFinishPalette ucSeamPallet)
        {
            if (ucSeamPallet.SeamFinishList != null)
            {
                ucSeamPallet.SeamFinishList.ForEach(s=>SeamPalletElementList.Add(new UCSeamPalletElementSerializable(s)));
            }
        }

    }
}
