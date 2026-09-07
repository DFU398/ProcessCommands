module CommandTests

open System.Collections.Generic
open ProcessCommands
open Xunit

[<Fact>]
let ``Returns correct exit code`` () =
    Assert.Equal(
        42,
        Command("sh", "-c \"exit 42\"").Run().ExitCode()
    )

[<Fact>]
let ``Returns text written to stdout`` () =
    Assert.Equal(
        "this is a test\n",
        Command("echo", "this is a test").Run().WithExitCode0().Output()
    )

[<Fact>]
let ``Returns text written to stderr`` () =
    Assert.Equal(
        "this is a test\n",
        Command("sh", "-c \"echo 'this is a test' >&2\"").Run().WithExitCode0().Error()
    )

[<Fact>]
let ``Runs in correct working directory`` () =
    Assert.Equal(
        "/home\n",
        Command(
            "/home",
            "sh",
            "-c \"echo $PWD\""
        ).Run().WithExitCode0().Output()
    )

[<Fact>]
let ``Runs with additional environment variables`` () =
    Assert.Contains(
        "QWER\n",
        Command(
            Dictionary(
                [ KeyValuePair("ASDF", "QWER") ]
            ),
            "sh",
            "-c \"echo $ASDF\""
        ).Run().WithExitCode0().Output()
    )

[<Fact>]
let ``Works with args list`` () =
    Assert.Equal(
        "this is a test\n",
        Command(
            "sh",
            ["-c"; "echo \"this is a test\""]
        ).Run().WithExitCode0().Output()
    )