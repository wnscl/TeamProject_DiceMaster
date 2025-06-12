
using JetBrains.Annotations;


public enum ColorName
{
    red,
    green,
    blue,
    cyan
}

    

  [UsedImplicitly]  
    public static class UIHelperTool
    {
        public static string GetColor(ColorName colorName)
        {
            switch (colorName)
            {
                case ColorName.red:
                    return  ColorName.red.ToString();
                case ColorName.green:
                    return  ColorName.green.ToString();
                case ColorName.blue:
                    return  ColorName.blue.ToString();
                case ColorName.cyan:
                    return ColorName.cyan.ToString(); ;
                default:
                    return  "black";
            }
        }


        public static string UIColorText(string text, ColorName colorName)
        {
            return $"<color={GetColor(colorName)}>{text}</color>";
        }
    }