module Main.Shell

open Avalonia.FuncUI.Components.Hosts
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Core.Input
open Elmish
open Main
open Avalonia.FuncUI.Elmish
open Core.State
open Main.Update

/// håll ett record for states
let shellInit: ShellState * Cmd<ShellMessage> =
  // om init kor ett command så kommer det här inte att funka, t.ex hämta data
  let exampleInit, exampleCmd = Core.Init.exampleInit
  let testInit, testCmd = Core.Init.testInit
  let debugInit, debugCmd = Core.Init.debugInit()
  {
    Core.State.Example = exampleInit
    Core.State.Test = testInit
    Core.State.Debug = debugInit
  },
  Cmd.batch [exampleCmd]
/// updates
let shellUpdate (message:ShellMessage) (state:ShellState) : ShellState * Cmd<ShellMessage> =
  match message with
  | TestPageMessage message ->
    let newState, returnMessage = TestPage.update message state
    let updatedState = {state with Test = newState}
    (updatedState, returnMessage)
  | ExamplePageMessage message ->
    let newState, returnMessage = ExampleUpdate.update message state
    {state with Example = newState}, returnMessage
  | DebugPageMessage message ->
    let newState, returnMessage = DebugUpdate.update message state
    {state with Debug = newState}, returnMessage

/// view
let shellView (shellState: ShellState) (dispatch: ShellMessage -> unit) =
  DockPanel.create [
    DockPanel.children [
      TabControl.create [
        TabControl.tabStripPlacement Dock.Top
        TabControl.viewItems [
          TabItem.create [
            TabItem.header "Example"
            TabItem.content (View.ExampleView.view shellState (ExamplePageMessage >> dispatch))
          ]
          TabItem.create [
            TabItem.header "EmptyTab"
            TabItem.content (View.TestView.view shellState (TestPageMessage >> dispatch))
          ]
          TabItem.create [
            TabItem.header "Debug"
            TabItem.content (View.DebugView.view shellState (DebugPageMessage >> dispatch))
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
        |> Program.withSubscription ExampleUpdate.timer
        |> Program.withSubscription DebugUpdate.timer
        |> Program.withConsoleTrace
        |> Program.run

