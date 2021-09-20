module ExamplePage

open System
open Avalonia.FuncUI.DSL
open Avalonia.Threading
open Main.Message

open Avalonia.Controls
open Avalonia.Layout
open Elmish


type State = { count: int; running: bool  }
let timer (state) = // exempel på timer som kors från ui
  let sub (dispatch: ShellMessage -> unit) = // subscriber
    let invoke() =
      (ShellMessage.TestPageMessage IncrementIfRunning) |> dispatch // vad som invokeas
      true // fortsätta korareturnms
    DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds 1000.) |> ignore // i princip en async.sleep 
  Cmd.ofSub sub // skickar command med en subrutin
let init =
  let initState = { count = 0; running = false }
  initState, Cmd.none
let incrementDelayedCmd (dispatch: ShellMessage -> unit) =
  let delayedDispatch =
    async {
      do! Async.Sleep 1000
      dispatch (ShellMessage.TestPageMessage Increment)
    }
  printfn "incrementDelayedCmdDispatch"
  Async.StartImmediate delayedDispatch
let update (msg: TestPageMessage) (state: State) =
  match msg with
  | Increment -> { state with count = state.count + 1 }, Cmd.none
  | Decrement -> { state with count = state.count - 1 }, Cmd.none
  | GetState -> state, Cmd.none
  | IncrementIfRunning ->
    match state.running with
//      | true -> { state with count = state.count + 1 }, Cmd.none
    | true ->
      printfn "Running"
      state, Cmd.ofMsg (ShellMessage.TestPageMessage Increment)
    | false -> state, Cmd.none
  | Reset -> { state with count = 0 }, Cmd.none
  | IncrementDelayed -> state, (Cmd.ofSub incrementDelayedCmd)
  | RunningTrue -> { state with running = true }, Cmd.none
  | RunningFalse -> { state with running = false }, Cmd.none

let view (state: State) (dispatch: TestPageMessage -> unit) =
  DockPanel.create [ DockPanel.children [ Button.create [ Button.dock Dock.Bottom
                                                          Button.height 50.
                                                          Button.onClick (fun _ -> dispatch IncrementDelayed)
                                                          Button.content "incrementDelayed" ]
                                          Button.create [ Button.isVisible (not state.running)
                                                          Button.dock Dock.Bottom
                                                          Button.height 50.
                                                          Button.onClick (fun _ -> dispatch RunningTrue)
                                                          Button.content "runningTrue" ]
                                          Button.create [ Button.isVisible (state.running)
                                                          Button.dock Dock.Bottom
                                                          Button.height 50.
                                                          Button.onClick (fun _ -> dispatch RunningFalse)
                                                          Button.content "runningFalse" ]
                                          Button.create [ Button.height 50.
                                                          Button.dock Dock.Bottom
                                                          Button.onClick (fun _ -> dispatch Reset)
                                                          Button.content "reset" ]
                                          Button.create [ Button.height 50.
                                                          Button.dock Dock.Bottom
                                                          Button.onClick (fun _ -> dispatch Decrement)
                                                          Button.content "-" ]
                                          Button.create [ Button.dock Dock.Bottom
                                                          Button.onClick (fun _ -> dispatch Increment)
                                                          Button.content "+" ]
                                          TextBlock.create [ TextBlock.dock Dock.Top
                                                             TextBlock.fontSize 48.0
                                                             TextBlock.verticalAlignment VerticalAlignment.Center
                                                             TextBlock.horizontalAlignment HorizontalAlignment.Center
                                                             TextBlock.text (string state.running) ]
                                          TextBlock.create [ TextBlock.dock Dock.Top
                                                             TextBlock.fontSize 48.0
                                                             TextBlock.verticalAlignment VerticalAlignment.Center
                                                             TextBlock.horizontalAlignment HorizontalAlignment.Center
                                                             TextBlock.text (string state.count) ] ] ]
