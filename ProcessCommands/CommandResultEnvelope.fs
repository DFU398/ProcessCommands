namespace ProcessCommands

open System

/// <summary>
/// Envelope for an <see cref="ICommandResult"/>.
/// </summary>
[<AbstractClass>]
type CommandResultEnvelope(
    content: Lazy<ICommandResult>
) =
    /// <summary>
    /// Envelope for an <see cref="ICommandResult"/>.
    /// </summary>
    new(
        content: ICommandResult   
    ) =
        CommandResultEnvelope(
            Lazy<ICommandResult>(content)
        )
    
    /// <summary>
    /// Envelope for an <see cref="ICommandResult"/>.
    /// </summary>
    new(
        content: Func<ICommandResult>   
    ) =
        CommandResultEnvelope(
            Lazy<ICommandResult>(content)
        )
    
    interface ICommandResult with
        member this.Error() = content.Value.Error()
        member this.ExitCode() = content.Value.ExitCode()
        member this.Output() = content.Value.Output()
        member this.WithExitCode([<ParamArray>] validExitCodes) = content.Value.WithExitCode(validExitCodes)
        member this.WithExitCode0() = content.Value.WithExitCode0()
        member this.WithNoError() = content.Value.WithNoError()