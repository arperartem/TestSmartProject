using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "BoosterData", menuName = "BoosterData", order = 0)]
    public class BoosterData : ScriptableObject
    {
        [SerializeField] private BoosterType type;
        [SerializeField] private Sprite sprite;

        public BoosterType Type => type;

        public Sprite Sprite => sprite;
    }
}