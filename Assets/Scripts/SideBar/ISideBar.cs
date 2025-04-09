namespace SideBar
{
    public interface ISideBar
    {
        bool IsVisible { get; }
        bool IsAnimating { get; }
        void SetVisible(bool visible);
    }
}