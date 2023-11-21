using Modules.MazeGenerator.Data;

namespace Features.MazeManagement
{
    public interface IMazeManager
    {
        MazeData MazeData { get; }

        void ReportPlayerFailed();
    }
}