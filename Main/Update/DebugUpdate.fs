module Main.Update.DebugUpdate

open System
open Avalonia.Threading
open Core.Input
open Core.State
open Elmish

let update (message:Core.Input.DebugInput) (state:ShellState) =
  match message with
  | DebugInput.Update ->
    let updatedState =
      {state.debug with messages = state.debug.manager.Get() |> Async.RunSynchronously}
    updatedState, Cmd.none
  | DebugInput.Add str ->
    state.debug.manager.Send str
    let cmdMessage = Core.Input.ShellMessage.DebugPageMessage Core.Input.DebugInput.Update
    state.debug, Cmd.ofMsg (cmdMessage)

let timer (state:ShellState) = // exempel på timer som kors från ui
  let manager = state
  let sub (dispatch: ShellMessage -> unit) = // subscriber
    let invoke() =
      (DebugPageMessage Update) |> dispatch // vad som invokeas
      true // fortsätta korareturnms
    DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds 10.) |> ignore // i princip en async.sleep 
  Cmd.ofSub sub // skickar command med en subrutin

