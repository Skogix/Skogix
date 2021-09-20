module Main.DebugPage

open System
open Avalonia.Threading
open Core.Input
open Core.State
open Elmish

let update message (state:ShellState) =
  match message with
  | Update ->
    let newMessages = {state.Debug with messages = state.Debug.messages}
    state.Debug, Cmd.none

let timer (state:ShellState) = // exempel på timer som kors från ui
  let manager = state
  let sub (dispatch: ShellMessage -> unit) = // subscriber
    let invoke() =
      (DebugPageMessage Update) |> dispatch // vad som invokeas
      true // fortsätta korareturnms
    DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds 10.) |> ignore // i princip en async.sleep 
  Cmd.ofSub sub // skickar command med en subrutin
