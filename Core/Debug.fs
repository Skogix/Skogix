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
type DebugData = DateTime * string
let debugShellMessage (msg: ShellMessage) =
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
/// debugMessage: ShellMessage * ShellState -> ShellMessage * ShellState
/// uppdaterar state med ny debug
let debugMessage (message, state:Core.State.ShellState) =
  let formattedMessage = debugShellMessage message

  let newDebugState =
    { state.debug with
        messages = (formattedMessage :: state.debug.messages)
    }

  let newState = { state with debug = newDebugState }
  (message, newState)
