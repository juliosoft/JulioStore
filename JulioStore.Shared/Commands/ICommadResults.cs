namespace JulioStore.Shared.Commands
{
    public interface ICommandResults
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}