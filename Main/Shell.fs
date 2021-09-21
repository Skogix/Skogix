module Main.Shell

open Elmish
open Main.Update
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Core.Input
open Main
open Core.State
open Main.Update
/// Shell.fs
/// en wrapper-modul som låter core-moduler kommunicera
/// är ansvarig for all ui-data, routeing, meny / persistent ui-element

/// shellUpdate: ShellMessage -> ShellState -> (ShellState * Cmd<ShellMessage>)
let handleMessage (message, state) = 
  match message with
  | TicTacToeMessage message ->
    let newState, returnMessage = TicTacToeUpdate.update message state
    {state with ticTacToe = newState}, returnMessage
  | TestPageMessage message ->
    let newState, returnMessage = TestPage.update message state
    ({state with test = newState}, returnMessage)
  | ExamplePageMessage message ->
    let newState, returnMessage = ExampleUpdate.update message state
    {state with example = newState}, returnMessage
  | DebugPageMessage message ->
    let newState, returnMessage = DebugUpdate.update message state
    {state with debug = newState}, returnMessage
let debugMessage (message, state) =
  let formattedMessage = Core.Debug.debugShellMessage message
  let newDebugState = {state.debug with messages = (formattedMessage :: state.debug.messages)}
  let newState = {state with debug = newDebugState}
  (message, newState)
let shellUpdate message (state:ShellState) =
  // todo skicka ett cmd med nya debug
//  Core.Debug.debugShellMessage message
  (message, state)
  |> debugMessage
  |> handleMessage
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
            TabItem.content (View.TicTacToeView.view shellState (TicTacToeMessage >> dispatch))
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
