using UnityEngine.UI;

namespace SideBar
{
    public interface ICellView
    {
        Image Icon { get; }

        void PlayFly();
    }
}