module Core.Example.Update

open System
open Avalonia.Threading
open Core.State
open Elmish

let incrementCounter state = { state with count = state.count + 1 }
let decrementCounter state = { state with count = state.count - 1 }
let runningTrue state = { state with running = true }
let runningFalse state = { state with running = false }

let timer (state) = // exempel på timer som kors från ui
  let sub (dispatch: Core.Input.ShellMessage -> unit) = // subscriber
    let invoke () =
      (Core.Input.ExamplePageMessage Core.Input.IncrementIfRunning)
      |> dispatch // vad som invokeas

//      true // fortsätta korareturnms
      state.running

    DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds 500.)
    |> ignore // i princip en async.sleep

  Cmd.ofSub sub // skickar command med en subrutin
