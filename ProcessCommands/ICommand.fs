namespace ProcessCommands

open System.Threading.Tasks

/// <summary>
/// A command that can be run.
/// </summary>
type ICommand =
    /// <summary>
    /// Run the command synchronously.
    /// </summary>
    abstract member Run: unit -> ICommandResult
    
    /// <summary>
    /// Run the command asynchronously.
    /// </summary>
    abstract member RunAsync: unit -> Task<ICommandResult>