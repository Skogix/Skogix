module Main.Route.Example

open System
open Avalonia.Threading
open Core.Input
open Core.State
open Elmish
open Core.Example



let incrementDelayedCmd (dispatch: ShellMessage -> unit) =
  let delayedDispatch =
    async {
      do! Async.Sleep 500
      dispatch (ExamplePageMessage Increment)
    }

  Async.StartImmediate delayedDispatch




let update (msg: ExampleInput) (shellState: ShellState) =
  let state = shellState.example

  match msg with
  | Increment -> Update.incrementCounter state, Cmd.none
  | Decrement -> Update.decrementCounter state, Cmd.none
  | IncrementIfRunning ->
    match state.running with
    | true -> state, Cmd.ofMsg (ExamplePageMessage Increment)
    | false -> state, Cmd.none
  | ResetCount -> { state with count = 0 }, Cmd.none
  | IncrementDelayed -> state, (Cmd.ofSub incrementDelayedCmd)
  | RunningTrue ->
    let newState = Update.runningTrue state
    newState, Update.timer newState
  | RunningFalse ->
    let newState = Update.runningFalse state
    newState, Update.timer newState
