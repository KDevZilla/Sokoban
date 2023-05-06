using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Skin
{
    public class SkinFactory
    {
        public enum SkinType
        {
            BoxWorldClassic,
            Boxxle,
            BoxxleColor,
         //   Chocoban,
         //   Lego,
            Yoshi32
        };
        private static Dictionary<String, SkinType> dicStringToSkinType = new Dictionary<string, SkinType>()
        {
            { "BoxWorldClassic",SkinType.BoxWorldClassic },
            { "Boxxle",SkinType.Boxxle },
            { "BoxxleColor",SkinType.BoxxleColor },
        //    { "Chocoban",SkinType.Chocoban },
        //    { "Lego",SkinType.Lego },
            { "Yoshi",SkinType.Yoshi32 }

        };

        private static Dictionary<SkinType, String> dicSkinTypeToString = new Dictionary<SkinType, String>()
        {
            { SkinType.BoxWorldClassic,"BoxWorldClassic"},
            { SkinType.Boxxle,"Boxxle" },
            { SkinType.BoxxleColor,"BoxxleColor" },
         //   { SkinType.Chocoban,"Chocoban" },
         //   { SkinType.Lego,"Lego" },
            { SkinType.Yoshi32,"Yoshi" }

        };
        public static SkinType FromStringToSkinType(String skintypeString)
        {
            return dicStringToSkinType[skintypeString];
        }
        public static String FromSkinTypeToString(SkinType skinType)
        {
            return dicSkinTypeToString[skinType];
        }
        public static BaseSkin CreateSkin(String skintype)
        {
            return CreateSkin(dicStringToSkinType[skintype]);
        }
        public static BaseSkin CreateSkin(SkinType skintype)
        {
            BaseSkin Skin = null;
            switch (skintype)
            {
                case SkinType.BoxWorldClassic:
                    Skin = new BoxWorldClassicSkin();
                    break;
                case SkinType.Boxxle:
                    Skin = new BoxxleSkin();
                    break;
                case SkinType.BoxxleColor:
                    Skin = new BoxxleColorSkin();
                    break;
                    /*
                case SkinType.Chocoban:
                    Skin = new ChocobanSkin();
                    break;
                case SkinType.Lego:
                    Skin = new LegoSkin();
                    break;
                    */
                case SkinType.Yoshi32:
                    Skin = new YoshiSkin();
                    break;
               
            }
            return Skin;
        }
    }
}
