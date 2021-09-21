module Main.Route.Debug

open System
open Avalonia.Threading
open Core.Input
open Core.State
open Elmish

let timer (state: ShellState) = // exempel på timer som kors från ui
  let sub (dispatch: ShellMessage -> unit) = // subscriber
    let invoke () =
      (DebugPageMessage Update) |> dispatch // vad som invokeas
      state.debug.enabled
      true
    //      true
    DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds 10.)
    |> ignore // i princip en async.sleep

  Cmd.ofSub sub // skickar command med en subrutin

let update (message: DebugInput) (shellState: ShellState) =
  let debugState, cmd =
    match message with
    | DebugInput.Start ->
      printfn "STAAAAAAART!"
      { shellState.debug with enabled = true }, Cmd.none
    | DebugInput.Add str ->
      let newMessages = str :: shellState.debug.messages

      let newState =
        { shellState.debug with
            messages = newMessages
        }

      newState, Cmd.none

  debugState, cmd
