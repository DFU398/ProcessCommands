namespace ProcessCommands

open System
open System.Collections.Generic
open System.Diagnostics
open System.Threading.Tasks

/// <summary>
/// A command that can be run.
/// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
/// </summary>
/// <param name="processStartInfo">Process start info for the command to run.</param>
[<Sealed>]
type Command(
    processStartInfo: Lazy<ProcessStartInfo>
) =
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="processStartInfo">Process start info for the command to run.</param>
    new(
        processStartInfo: ProcessStartInfo
    ) =
        Command(
            Lazy<ProcessStartInfo>(processStartInfo)
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
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
        Command(
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
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
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
        Command(
            processStartInfo,
            workDir,
            Dictionary<string, string>(),
            fileName,
            arguments
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
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
        Command(
            processStartInfo,
            "",
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        fileName: string,
        arguments: string
    ) =
        Command(
            processStartInfo,
            "",
            Dictionary<string, string>(),
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
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
        Command(
            ProcessStartInfo(),
            workDir,
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        workDir: string,
        fileName: string,
        arguments: string
    ) =
        Command(
            ProcessStartInfo(
                fileName,
                arguments,
                WorkingDirectory=workDir
            )
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: string
    ) =
        Command(
            ProcessStartInfo(),
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        fileName: string,
        arguments: string
    ) =
        Command(
            ProcessStartInfo(fileName, arguments)
        )
        
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
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
        Command(
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
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
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
        Command(
            processStartInfo,
            workDir,
            Dictionary<string, string>(),
            fileName,
            arguments
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
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
        Command(
            processStartInfo,
            "",
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="processStartInfo">Additional process start info for the command to run.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        processStartInfo: ProcessStartInfo,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        Command(
            processStartInfo,
            "",
            Dictionary<string, string>(),
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
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
        Command(
            ProcessStartInfo(),
            workDir,
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="workDir">Working directory in which to run the command.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        workDir: string,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        Command(
            ProcessStartInfo(
                fileName,
                arguments,
                WorkingDirectory=workDir
            )
        )
    
    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="environmentVars">Additional environment variables to run the command with.</param>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        environmentVars: IDictionary<string, string>,
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        Command(
            ProcessStartInfo(),
            environmentVars,
            fileName,
            arguments
        )

    /// <summary>
    /// A command that can be run.
    /// Sets <see cref="ProcessStartInfo.UseShellExecute"/> to false, so the executable is run directly.
    /// </summary>
    /// <param name="fileName">The file name of the executable to run.</param>
    /// <param name="arguments">Arguments to run the command with.</param>
    new(
        fileName: string,
        arguments: IEnumerable<string>
    ) =
        Command(
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
                info.RedirectStandardOutput <- true
                info.RedirectStandardError <- true
                info.UseShellExecute <- false
                
                let proc =
                    Process.Start(
                        processStartInfo.Value
                    )
                
                let outputTask = proc.StandardOutput.ReadToEndAsync()
                let errorTask = proc.StandardError.ReadToEndAsync()
                
                let! _ = proc.WaitForExitAsync() |> Async.AwaitTask
                let! output = outputTask |> Async.AwaitTask
                let! error = errorTask |> Async.AwaitTask
                
                return CommandResult(output, error, proc.ExitCode) :> ICommandResult
            }