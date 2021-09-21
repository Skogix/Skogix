module Main.Route.Example

open System
open Avalonia.Threading
open Core.Input
open Core.State
open Elmish
open Core.Example


let timer state = // exempel på timer som kors från ui
  let sub (dispatch: ShellMessage -> unit) = // subscriber
    let invoke () =
      (ExamplePageMessage IncrementIfRunning)
      |> dispatch // vad som invokeas

      true // fortsätta korareturnms

    DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds 3000.)
    |> ignore // i princip en async.sleep

  Cmd.ofSub sub // skickar command med en subrutin

let incrementDelayedCmd (dispatch: ShellMessage -> unit) =
  let delayedDispatch =
    async {
      do! Async.Sleep 3000
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
  | RunningTrue -> Update.runningTrue state, Cmd.none
  | RunningFalse -> Update.runningFalse state, Cmd.none
