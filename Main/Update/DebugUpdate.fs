module Main.Update.DebugUpdate

open System
open Avalonia.Threading
open Core.Input
open Core.State
open Elmish

let timer (state:ShellState) = // exempel på timer som kors från ui
  let sub (dispatch: ShellMessage -> unit) = // subscriber
    let invoke() =
      (DebugPageMessage Update) |> dispatch // vad som invokeas
      state.debug.enabled
      true
//      true
    DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds 10.) |> ignore // i princip en async.sleep 
  Cmd.ofSub sub // skickar command med en subrutin
let update (message:Core.Input.DebugInput) (shellState:ShellState) =
  let updatedState = {shellState.debug with messages = shellState.debug.manager.Get() |> Async.RunSynchronously}
  let debugState, cmd = 
    match message with
    | DebugInput.Start ->
      printfn "STAAAAAAART!"
      {shellState.debug with enabled = true}, Cmd.none
    | DebugInput.Update ->
      updatedState, Cmd.none
    | DebugInput.Add str ->
      shellState.debug.manager.Send str
      let cmdMessage = Core.Input.ShellMessage.DebugPageMessage Core.Input.DebugInput.Update
      updatedState, Cmd.ofMsg (cmdMessage)
  debugState, cmd


