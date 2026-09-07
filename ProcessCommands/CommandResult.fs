namespace ProcessCommands

open System
open System.Linq

/// <summary>
/// Result of running a command.
/// </summary>
/// <param name="output">Text that was written to stdout.</param>
/// <param name="error">Text that was written to stderr.</param>
/// <param name="exitCode">The exit code of the process.</param>
[<Sealed>]
type CommandResult(
    output: Lazy<string>,
    error: Lazy<string>,
    exitCode: Lazy<int>
) =
    /// <summary>
    /// Result of running a command.
    /// </summary>
    /// <param name="output">Text that was written to stdout.</param>
    /// <param name="error">Text that was written to stderr.</param>
    /// <param name="exitCode">The exit code of the process.</param>
    new(
        output: string,
        error: string,
        exitCode: int
    ) =
        CommandResult(
            Lazy<string>(output),
            Lazy<string>(error),
            Lazy<int>(exitCode)
        )
    
    interface ICommandResult with
        member this.Error() = error.Value
        member this.ExitCode() = exitCode.Value
        member this.Output() = output.Value
        
        member this.WithExitCode0() : ICommandResult =
            this :> ICommandResult |> _.WithExitCode(0)
        
        member this.WithExitCode([<ParamArray>] validExitCodes: int[]) : ICommandResult =
            if validExitCodes.Length = 0 then
                raise(
                    ArgumentException(
                        "Failed to validate exit code. No valid exit codes were specified."
                    )
                )
                
            if not (validExitCodes.Contains(exitCode.Value)) then
                raise(
                    ApplicationException(
                        $"Failed to run command. Process returned unexpected exit code {exitCode.Value}."
                        + (if error.Value.Length > 0 then "\n" + error.Value else "")
                    )
                )
            
            this :> ICommandResult
            
        member this.WithNoError() : ICommandResult =
            if error.Value.Length > 0 then
                raise(
                    ApplicationException(
                        $"Failed to run command. Process returned unexpected output on stderr: {error.Value}"
                    )
                )
            
            this :> ICommandResult