using UnityEngine;

namespace CommonUI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        private RectTransform _panel;

        private RectTransform Panel => _panel ??= GetComponent<RectTransform>();

        private Rect _lastSafeArea = Rect.zero;
        private ScreenOrientation _lastOrientation = ScreenOrientation.AutoRotation;

        private void Awake()
        {
            ApplySafeArea();
        }

        private void OnEnable()
        {
            ApplySafeArea();
        }

        private void OnRectTransformDimensionsChange() //for device simulator
        {
            ApplySafeAreaIfChanged();
        }

        private void Update()
        {
#if !UNITY_EDITOR
            ApplySafeAreaIfChanged();
#endif
        }

        private void ApplySafeAreaIfChanged()
        {
            if (_lastSafeArea != Screen.safeArea || _lastOrientation != Screen.orientation)
            {
                ApplySafeArea();
            }
        }

        private void ApplySafeArea()
        {
            var safeArea = Screen.safeArea;
            _lastSafeArea = safeArea;
            _lastOrientation = Screen.orientation;

            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            Panel.anchorMin = anchorMin;
            Panel.anchorMax = anchorMax;
        }
    }
}