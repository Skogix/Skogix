module Main.Shell

open Elmish
open Main.Update
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Core.Input
open Main
open Core.State
/// Shell.fs
/// en wrapper-modul som låter core-moduler kommunicera
/// är ansvarig for all ui-data, routeing, meny / persistent ui-element

/// shellInit: ShellState * Cmd<ShellMessage>
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
/// shellUpdate: ShellMessage -> ShellState -> (ShellState * Cmd<ShellMessage>)
let shellUpdate message state =
  Core.Debug.debugShellMessage message
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

/// shellView: ShellState -> (ShellMessage -> unit) -> IView<DockPanel>
/// är i praktiken just nu bara menyn
let shellView (shellState: ShellState) (dispatch: ShellMessage -> unit) =
  DockPanel.create [
    DockPanel.children [
      TabControl.create [
        TabControl.tabStripPlacement Dock.Top
        TabControl.viewItems [
          TabItem.create [
            TabItem.header "TicTacToe"
            TabItem.content (View.TicTacToeView.view shellState (DebugPageMessage >> dispatch))
          ]
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
