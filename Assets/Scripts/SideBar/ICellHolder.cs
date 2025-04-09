using System.Collections.Generic;

namespace SideBar
{
    public interface ICellHolder
    {
        public List<ICellView> Cells { get; }

        ICellView GetFirstAvailableCell();
    }
}