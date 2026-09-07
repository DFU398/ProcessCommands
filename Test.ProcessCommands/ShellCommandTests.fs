module ShellCommandTests

open System.Collections.Generic
open ProcessCommands
open Xunit

[<Fact>]
let ``Returns correct exit code`` () =
    Assert.Equal(
        42,
        ShellCommand("sh", "-c \"exit 42\"").Run().ExitCode()
    )

[<Fact>]
let ``Runs in correct working directory`` () =
    Assert.Equal(
        0,
        ShellCommand(
            "/home",
            "sh",
            "-c \"test $PWD = /home\""
        ).Run().ExitCode()
    )

[<Fact>]
let ``Runs with additional environment variables`` () =
    Assert.Equal(
        42,
        ShellCommand(
            Dictionary(
                [ KeyValuePair("ASDF", "42") ]
            ),
            "sh",
            "-c \"exit $ASDF\""
        ).Run().ExitCode()
    )

[<Fact>]
let ``Works with args list`` () =
    Assert.Equal(
        42,
        ShellCommand("sh", ["-c"; "exit 42"]).Run().ExitCode()
    )