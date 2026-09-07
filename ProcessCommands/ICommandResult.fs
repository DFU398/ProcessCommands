namespace ProcessCommands

open System

/// <summary>
/// Result of running a command.
/// </summary>
type ICommandResult =
    /// <summary>
    /// The exit code of the process.
    /// </summary>
    abstract member ExitCode: unit -> int
    
    /// <summary>
    /// Text that was written to stdout.
    /// </summary>
    abstract member Output: unit -> string
    
    /// <summary>
    /// Text that was written to stderr.
    /// </summary>
    abstract member Error: unit -> string
    
    /// <summary>
    /// Throws an exception if the exit code was not a valid value.
    /// Returns the same command result.
    /// </summary>
    abstract member WithExitCode: [<ParamArray>] validExitCodes: int[] -> ICommandResult
    
    /// <summary>
    /// Throws an exception if the exit code was not 0.
    /// Returns the same command result.
    /// </summary>
    abstract member WithExitCode0: unit -> ICommandResult
    
    /// <summary>
    /// Throws an exception if something was written to stderr.
    /// Returns the same command result.
    /// </summary>
    abstract member WithNoError: unit -> ICommandResult