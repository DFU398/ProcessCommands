module CommandResultTests

open System
open ProcessCommands
open Xunit

[<Fact>]
let ``Throws on invalid exit code`` () =
    Assert.Equal(
        "Failed to run command. Process returned unexpected exit code -1.",
        Assert.Throws<ApplicationException>(
            fun () ->
                ignore(
                    CommandResult("", "", -1) :> ICommandResult |> _.WithExitCode0()
                )
        ).Message
    )

[<Fact>]
let ``Does not throw on valid exit code`` () =
    ignore(
        CommandResult("", "", 0) :> ICommandResult |> _.WithExitCode0()
    )

[<Fact>]
let ``Throws on invalid exit code with custom valid exit codes`` () =
    Assert.Equal(
        "Failed to run command. Process returned unexpected exit code 0.",
        Assert.Throws<ApplicationException>(
            fun () ->
                ignore(
                    CommandResult("", "", 0) :> ICommandResult |> _.WithExitCode(1337)
                )
        ).Message
    )

[<Fact>]
let ``Does not throw on custom valid exit code`` () =
    ignore(
        CommandResult("", "", 1337) :> ICommandResult |> _.WithExitCode(1337)
    )

[<Fact>]
let ``Throws on no valid exit codes specified`` () =
    Assert.Equal(
        "Failed to validate exit code. No valid exit codes were specified.",
        Assert.Throws<ArgumentException>(
            fun () ->
                ignore(
                    CommandResult("", "", 0) :> ICommandResult |> _.WithExitCode()
                )
        ).Message
    )

[<Fact>]
let ``Throws on error stream output`` () =
    Assert.Equal(
        "Failed to run command. Process returned unexpected output on stderr: oops",
        Assert.Throws<ApplicationException>(
            fun () ->
                ignore(
                    CommandResult("", "oops", 0) :> ICommandResult |> _.WithNoError()
                )
        ).Message
    )

[<Fact>]
let ``Does not throw on empty error stream output`` () =
    ignore(
        CommandResult("", "", 0) :> ICommandResult |> _.WithNoError()
    )