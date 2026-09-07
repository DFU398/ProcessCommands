namespace ProcessCommands

open System

/// <summary>
/// Envelope for an <see cref="ICommand"/>.
/// </summary>
[<AbstractClass>]
type CommandEnvelope(
    content: Lazy<ICommand>
) =
    /// <summary>
    /// Envelope for an <see cref="ICommand"/>.
    /// </summary>
    new(
        content: ICommand   
    ) =
        CommandEnvelope(
            Lazy<ICommand>(content)
        )
    
    /// <summary>
    /// Envelope for an <see cref="ICommand"/>.
    /// </summary>
    new(
        content: Func<ICommand>   
    ) =
        CommandEnvelope(
            Lazy<ICommand>(content)
        )
    
    interface ICommand with
        member this.Run() = content.Value.Run()
        member this.RunAsync() = content.Value.RunAsync()