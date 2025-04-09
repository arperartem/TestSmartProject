namespace SideBar
{
    public interface ISideBar
    {
        bool IsVisible { get; }
        void SetVisible(bool visible);
    }
}