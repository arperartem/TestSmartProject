using UnityEngine;
using UnityEngine.UI;

public static class UIExtensions
{
    public static void SetFlipX(this RectTransform rt, bool flip)
    {
        var scale = rt.localScale;
        scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        rt.localScale = scale;
    }
    
    public static void SetAlpha(this MaskableGraphic image, float value)
    {
        var color = image.color;
        color.a = value;
        image.color = color;
    }
}