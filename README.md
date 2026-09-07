![Tests](https://github.com/DFU398/ProcessCommands/actions/workflows/tests.yml/badge.svg?branch=main)
[![EO principles respected here](http://www.elegantobjects.org/badge.svg)](http://www.elegantobjects.org)

# ProcessCommands
Convenience wrapper around System.Diagnostics.Process.Start for easily running another executable from your .NET code.

1. [What is this](#what-is-this)
2. [Installation](#installation) 
3. [Concepts](#concepts)
   1. [Commands](#commands)
   2. [Command Results](#command-results)
4. [Usage Examples](#usage-examples)

---
# What is this
In most shell scripting languages there is an easy way to run an executable and read its output.
For example, say you want to read the username from your git config: In posix shell scripts you can do
`USERNAME=$(git config user.name)`, in powershell you can do `$username = & git config user.name`.

F# can be used as a scripting language via `dotnet fsi your-script-name.fsx`, but since the language seems to be intended
more as a programming language than a scripting language, it lacks a convenient way to do the above.
Instead you would have to do something like this:
```fsharp
let username =
    Process.Start(
        ProcessStartInfo("git", "config user.name", RedirectStandardOutput=true)
    ).StandardOutput.ReadToEnd()
```
If it is possible that the command didn't succeed, and you want to fail fast instead of waiting for followup errors,
you would have to check the exit code:
```fsharp
use proc =
    Process.Start(
        ProcessStartInfo("git", "config user.name", RedirectStandardOutput=true)
    )
use outputTask = proc.StandardOutput.ReadToEndAsync()
proc.WaitForExit()
if proc.ExitCode <> 0 then
    raise(Exception("something went wrong"))
let username = outputTask.Result
```
That is inconvenient. With this library, you can instead do it like this:
```fsharp
let username = Command("git", "config user.name").Run().Output()
```
Or alternatively, check the exit code to fail fast if the command failed:
```fsharp
let username = Command("git", "config user.name").Run().WithExitCode0().Output()
```

---
# Installation
To add the nuget package to your C# of F# project, run this in the directory containing the .csproj or .fsproj file:
```
dotnet package add ProcessCommands@1.0.0
```
To reference the nuget package in an F# script, add this line at the beginning of your script:
```fsharp
#r "nuget: ProcessCommands, 1.0.0"
```

---
# Concepts
## Commands
A command is something that can be executed. You instantiate a command with information about which executable to use,
which arguments to pass to the executable, etc., and then you can run it synchronously or asynchronously:
```fsharp
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
```
There are two implementations of ICommand. The `Command` class is the default way to run a command.
Alternatively, there is the `ShellCommand` class, which sets [ProcessStartInfo.UseShellExecute](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.useshellexecute?view=net-8.0)
to true. This means that the shell is used to start the process instead of starting it directly from the executable.
It also means that `ShellCommand` can be used to open a file in the default application for that file type,
while `Command` can only be used to start executables.

`ShellCommand` exists as a separate class, because setting `UseShellExecute` to true would not be possible
in the `Command` class. `Command` redirects the stdout and stderr streams to capture any text written to them,
and .NET does not allow stream redirection when `UseShellExecute` is true.
This also means that `ShellCommand` can not capture text written to stdout and stderr and will always return an
`ICommandResult` containing empty strings for the output and error text.

## Command Results
A command result is returned after a command has finished running. It contains the exit code of the process,
any text that was written to stdout and any text that was written to stderr.
In addition to that, there are built in methods to validate the exit code and to validate that nothing was written to stderr:
```fsharp
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
```

---
# Usage Examples
Run a command and ignore the result:
```csharp
// C#:
new Command("git", "config user.name").Run();
```
```fsharp
// F#:
Command("git", "config user.name").Run()
```

Run a command and read the result:
```csharp
// C#:
var username = new Command("git", "config user.name").Run().Output();
```
```fsharp
// F#:
let username = Command("git", "config user.name").Run().Output()
```

Run a command and read text written to stderr:
```csharp
// C#:
var error = new Command("git", "config user.name").Run().Error();
```
```fsharp
// F#:
let error = Command("git", "config user.name").Run().Error()
```

Run a command asynchronously and read the result:
```csharp
// C#:
var task = new Command("git", "config user.name").RunAsync();
// ...
// do something else in the meantime
// ...
var username = task.Result.Output();
```
```fsharp
// F#:
let task = Command("git", "config user.name").RunAsync()
// ...
// do something else in the meantime
// ...
let username = task.Result.Output()
```

Run a command, validate that it exited with code 0 and read the result:
```csharp
// C#:
var username = new Command("git", "config user.name").Run().WithExitCode0().Output();
```
```fsharp
// F#:
let username = Command("git", "config user.name").Run().WithExitCode0().Output()
```

Run a command, validate that nothing was written to stderr and read the result:
```csharp
// C#:
var username = new Command("git", "config user.name").Run().WithNoError().Output();
```
```fsharp
// F#:
let username = Command("git", "config user.name").Run().WithNoError().Output()
```

Run a command in a specific working directory:
```csharp
// C#:
new Command("/path/to/workingDir", "git", "config user.name").Run();
```
```fsharp
// F#:
Command("/path/to/workingDir", "git", "config user.name").Run()
```

Run a command with additional environment variables:
```csharp
// C#:
new Command(
    new Dictionary() { { "ASDF", "QWER" } },
    "sh",
    "-c \"echo $ASDF\""
).Run();
```
```fsharp
// F#:
Command(
    Dictionary([ KeyValuePair("ASDF", "QWER") ]),
    "sh",
    "-c \"echo $ASDF\""
).Run()
```

Run a command asynchronously, in a specific working directory, with additional environment variables,
validate that it exited with code 0 and nothing was written to stderr, and read the text written to stderr and stdout:
```csharp
// C#:
var task =
    new Command(
        "/path/to/workingDir",
        Dictionary() { { "ASDF", "QWER" } },
        "sh",
        "-c \"echo $ASDF\""
    ).RunAsync();
// ...
// do something else in the meantime
// ...
var result = task.Result.WithExitCode0().WithNoError();
var output = result.Output();
var error = result.Error(); // Yes, this is pointless after calling .WithNoError(),
                            // but it's something you can do. The string here will be empty,
                            // otherwise .WithNoError() would have thrown an exception.
```
```fsharp
// F#:
let task =
    Command(
        "/path/to/workingDir",
        Dictionary([ KeyValuePair("ASDF", "QWER") ]),
        "sh",
        "-c \"echo $ASDF\""
    ).RunAsync()
// ...
// do something else in the meantime
// ...
let result = task.Result.WithExitCode0().WithNoError()
let output = result.Output()
let error = result.Error() // yes, this is pointless after calling .WithNoError(),
                           // but it's something you can do. The string here will be empty,
                           // otherwise .WithNoError() would have thrown an exception.
```