module Main.Shell

open Avalonia.FuncUI.Components.Hosts
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Elmish
open Main
open Avalonia.FuncUI.Elmish
open Main.Message

/// håll ett record for states
type ShellState = {
  counterState: Counter.State
  emptyTabState: TestPage.State
}
/// shell-messages, d-union
  
/// hämta inits
let shellInit: ShellState * Cmd<ShellMessage> =
  // om init kor ett command så kommer det här inte att funka, t.ex hämta data
  let counterInitState, counterCmd = Counter.init
  let emptyTabInitState, emptyTabCmd = TestPage.init
  {
    counterState = counterInitState
    emptyTabState = emptyTabInitState
  },
  Cmd.batch [counterCmd]
/// updates
let shellUpdate (message:ShellMessage) (state:ShellState) : ShellState * Cmd<ShellMessage> =
  match message with
  | TestPageMessage (message) ->
    let newState, returnMessage = Counter.update message state.counterState
    let updatedState = {state with counterState = newState}
    (updatedState, returnMessage)
  | ExamplePageMessage message ->
    let newState, returnMessage = TestPage.update message state.emptyTabState
    {state with emptyTabState = newState}, returnMessage

/// view
let shellView (state: ShellState) (dispatch: ShellMessage -> unit) =
  DockPanel.create [
    DockPanel.children [
      TabControl.create [
        TabControl.tabStripPlacement Dock.Top
        TabControl.viewItems [
          TabItem.create [
            TabItem.header "Counter"
            TabItem.content (Counter.view state.counterState (ShellMessage.TestPageMessage >> dispatch))
          ]
          TabItem.create [
            TabItem.header "EmptyTab"
            TabItem.content (TestPage.view state.emptyTabState (ShellMessage.ExamplePageMessage >> dispatch))
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

//        let timer (state:ShellState) =
//          let sub (dispatch: ShellMessage -> unit) =
//            let invoke() =
//              (Message.CounterMessage IncrementIfRunning) |> dispatch
////              true
//            DispatcherTimer.Run(Func<bool>(invoke), TimeSpan.FromMilliseconds(100.)) |> ignore
//          Cmd.ofSub sub

        Elmish.Program.mkProgram (fun _ -> shellInit) shellUpdate shellView
        |> Program.withHost this
//        |> Program.withSubscription timer
        |> Program.withSubscription Counter.timer
        |> Program.withConsoleTrace
        |> Program.run

