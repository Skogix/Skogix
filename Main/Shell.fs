module Main.Shell

open System
open Avalonia.FuncUI.Components.Hosts
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Threading
open Elmish
open Main
open Avalonia.FuncUI.Elmish
open Main.Message

/// håll ett record for states
type ShellState = {
  counterState: Counter.State
  emptyTabState: EmptyTab.State
}
/// shell-messages, d-union
  
/// hämta inits
let shellInit: ShellState * Cmd<ShellMessage> =
  // om init kor ett command så kommer det här inte att funka, t.ex hämta data
  let counterInitState, counterCmd = Counter.init
  let emptyTabInitState, emptyTabCmd = EmptyTab.init
  {
    counterState = counterInitState
    emptyTabState = emptyTabInitState
  },
  Cmd.batch [counterCmd]
/// updates
let shellUpdate (message:ShellMessage) (state:ShellState) : ShellState * Cmd<ShellMessage> =
  match message with
  | CounterMessage (message) ->
    let newState, returnMessage = Counter.update message state.counterState
    let updatedState = {state with counterState = newState}
    (updatedState, returnMessage)
  | EmptyTabMessage message ->
    let newState, returnMessage = EmptyTab.update message state.emptyTabState
    let newReturnMessage = (returnMessage)
    state, returnMessage

/// view
let shellView (state: ShellState) (dispatch: ShellMessage -> unit) =
  DockPanel.create [
    DockPanel.children [
      TabControl.create [
        TabControl.tabStripPlacement Dock.Top
        TabControl.viewItems [
          TabItem.create [
            TabItem.header "Counter"
            TabItem.content (Counter.view state.counterState (ShellMessage.CounterMessage >> dispatch))
          ]
          TabItem.create [
            TabItem.header "EmptyTab"
            TabItem.content (EmptyTab.view state.emptyTabState (ShellMessage.EmptyTabMessage >> dispatch))
          ]
        ]
      ]
    ]
  ]
type MainWindow() as this =
    inherit HostWindow()
    do
        base.Title <- "Main"
        base.Width <- 400.0
        base.Height <- 400.0
        
        this.VisualRoot.VisualRoot.Renderer.DrawFps <- true
        this.VisualRoot.VisualRoot.Renderer.DrawDirtyRects <- true

        let timer (state:ShellState) =
          let sub (dispatch: ShellMessage -> unit) =
            let invoke() =
              (Message.CounterMessage IncrementIfRunning) |> dispatch
              false
            DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds(100.)) |> ignore
          Cmd.ofSub sub

        Elmish.Program.mkProgram (fun _ -> shellInit) shellUpdate shellView
        |> Program.withHost this
        |> Program.withSubscription timer
        |> Program.withConsoleTrace
        |> Program.run

