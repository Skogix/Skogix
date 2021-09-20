module Main.Shell

open Avalonia.FuncUI.Components.Hosts
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Core.Input
open Elmish
open Main
open Avalonia.FuncUI.Elmish

/// håll ett record for states
type ShellState = {
  exampleState: Core.Example.State.State
  testState: Core.Test.State.State
}
/// hämta inits
let shellInit: ShellState * Cmd<ShellMessage> =
  // om init kor ett command så kommer det här inte att funka, t.ex hämta data
  let counterInitState, counterCmd = Core.Example.State.init
  let emptyTabInitState, emptyTabCmd = Core.Test.State.init
  {
    exampleState = counterInitState
    testState = emptyTabInitState
  },
  Cmd.batch [counterCmd]
/// updates
let shellUpdate (message:ShellMessage) (state:ShellState) : ShellState * Cmd<ShellMessage> =
  match message with
  | TestPageMessage message ->
    let newState, returnMessage = TestPage.update message state.testState
    let updatedState = {state with testState = newState}
    (updatedState, returnMessage)
  | ExamplePageMessage message ->
    let newState, returnMessage = ExamplePage.update message state.exampleState
    {state with exampleState = newState}, returnMessage

/// view
let shellView (state: ShellState) (dispatch: ShellMessage -> unit) =
  DockPanel.create [
    DockPanel.children [
      TabControl.create [
        TabControl.tabStripPlacement Dock.Top
        TabControl.viewItems [
          TabItem.create [
            TabItem.header "Example"
            TabItem.content (View.ExampleView.view state.exampleState (ExamplePageMessage >> dispatch))
          ]
          TabItem.create [
            TabItem.header "EmptyTab"
            TabItem.content (View.TestView.view state.testState (TestPageMessage >> dispatch))
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

        Elmish.Program.mkProgram (fun _ -> shellInit) shellUpdate shellView
        |> Program.withHost this
//        |> Program.withSubscription timer
        |> Program.withSubscription ExamplePage.timer
        |> Program.withConsoleTrace
        |> Program.run

