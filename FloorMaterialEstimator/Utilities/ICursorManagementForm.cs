
namespace Utilities
{
    public interface ICursorManagementForm
    {
        bool CursorWithinBounds();

        void AddToCursorManagementList();

        void RemoveFromCursorManagementList();

        bool IsTopMost { get; set; }
    }
}
