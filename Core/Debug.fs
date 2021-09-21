module Core.Debug

open System
open Core.Input
/// ToDo
/// filtrering
/// har meddelandet skickats till output
/// fixa terminal-output

/// Debug.fs
/// hanterar all intern debugging
/// borde vara threadsafe

let debugShellMessage (msg:Core.Input.ShellMessage) =
  match msg with
  | TicTacToeMessage ticTacToeMessage ->
    match ticTacToeMessage with
    | TryPlaceMove i -> $"ticTacToe.TryPlaceMove {i}"
    | Reset -> $"ticTacToe.Reset"
  | ShellMessage.DebugPageMessage debugMessage ->
    match debugMessage with
    | Start -> "debug.Start"
    | Stop -> "debug.Stop"
    | Update -> ""
    | Add str -> $"debug.Add {str}"
  | ShellMessage.TestPageMessage testMessage -> ""
  | ShellMessage.ExamplePageMessage exampleMessage ->
    match exampleMessage with
    | IncrementIfRunning -> ""
    | Increment -> $"example.Increment"
    | IncrementDelayed -> $"example.IncrementDelayed"
    | Decrement -> $"example.Decrement"
    | ResetCount -> $"example.ResetCount"
    | RunningTrue -> $"example.RunningTrue"
    | RunningFalse -> $"example.RunningFalse"
