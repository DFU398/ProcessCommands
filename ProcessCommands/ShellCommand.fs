namespace ProcessCommands

open System
open System.Collections.Generic
open System.Diagnostics
open System.Threading.Tasks

/// <summary>
/// A command that can be run.
/// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
/// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
/// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
/// empty strings.
/// </summary>
/// <param name="processStartInfo">Process start info for the command to run.</param>
[<Sealed>]
type ShellCommand(
    processStartInfo: Lazy<ProcessStartInfo>
) =
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Process start info for the command to run.</param>
    new(
        processStartInfo: ProcessStartInfo
    ) =
        ShellCommand(
            Lazy<ProcessStartInfo>(processStartInfo)
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        workDir: string,
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: string
    ) =
        ShellCommand(
            Lazy<ProcessStartInfo>(
                fun () ->
                    processStartInfo.FileName <- fileName
                    processStartInfo.Arguments <- arguments
                    processStartInfo.WorkingDirectory <- workDir
                    for (kvp) in environmentVars do
                        processStartInfo.Environment[kvp.Key] <- kvp.Value
                    processStartInfo
            )
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        workDir: string,
        fileName: string,
        arguments: string
    ) =
        ShellCommand(
            processStartInfo,
            workDir,
            Dictionary<string, string>(),
            fileName,
            arguments
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: string
    ) =
        ShellCommand(
            processStartInfo,
            "",
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        fileName: string,
        arguments: string
    ) =
        ShellCommand(
            processStartInfo,
            "",
            Dictionary<string, string>(),
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        workDir: string,
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: string
    ) =
        ShellCommand(
            ProcessStartInfo(),
            workDir,
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        workDir: string,
        fileName: string,
        arguments: string
    ) =
        ShellCommand(
            ProcessStartInfo(
                fileName,
                arguments,
                WorkingDirectory=workDir
            )
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: string
    ) =
        ShellCommand(
            ProcessStartInfo(),
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        fileName: string,
        arguments: string
    ) =
        ShellCommand(
            ProcessStartInfo(fileName, arguments)
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        workDir: string,
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        ShellCommand(
            Lazy<ProcessStartInfo>(
                fun () ->
                    processStartInfo.FileName <- fileName
                    processStartInfo.WorkingDirectory <- workDir
                    for (kvp) in environmentVars do
                        processStartInfo.Environment[kvp.Key] <- kvp.Value
                    let argsList = processStartInfo.ArgumentList
                    argsList.Clear()
                    for arg in arguments do
                        argsList.Add(arg)
                    processStartInfo
            )
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        workDir: string,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        ShellCommand(
            processStartInfo,
            workDir,
            Dictionary<string, string>(),
            fileName,
            arguments
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        ShellCommand(
            processStartInfo,
            "",
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        ShellCommand(
            processStartInfo,
            "",
            Dictionary<string, string>(),
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        workDir: string,
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        ShellCommand(
            ProcessStartInfo(),
            workDir,
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        workDir: string,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        ShellCommand(
            ProcessStartInfo(
                fileName,
                arguments,
                WorkingDirectory=workDir
            )
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        ShellCommand(
            ProcessStartInfo(),
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to true, so the command is run via the shell.
    /// Dotnet doesn't let you redirect IO streams with <see cref="ProcessStartInfo.UseShellExecute"/> set to true,
    /// so this <see cref="ICommand"/> implementation always returns an <see cref="ICommandResult"/> containing
    /// empty strings.
    /// </summary>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        ShellCommand(
            ProcessStartInfo(fileName, arguments)
        )
        
    member this.Run() = this :> ICommand |> _.Run()
    member this.RunAsync() = this :> ICommand |> _.RunAsync()

    interface ICommand with
        member this.Run() : ICommandResult =
            (this :> ICommand).RunAsync().Result
            
        member this.RunAsync() : Task<ICommandResult> =
            task {
                let info = processStartInfo.Value
                info.RedirectStandardOutput <- false
                info.RedirectStandardError <- false
                info.UseShellExecute <- true
                
                let proc =
                    Process.Start(
                        processStartInfo.Value
                    )
                
                let! _ = proc.WaitForExitAsync() |> Async.AwaitTask
                
                return CommandResult("", "", proc.ExitCode) :> ICommandResult
            }
