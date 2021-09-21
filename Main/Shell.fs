module Main.Shell

open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Core.Input
open Main
open Core.State
open Main.Route
/// Shell.fs
/// en wrapper-modul som låter core-moduler kommunicera
/// är ansvarig for all ui-data, routeing, meny / persistent ui-element

/// handleMessage: ShellMessage * ShellState -> ShellState * Cmd<ShellMessage>
/// routear message till modul
let routeInput (message, state) =
  match message with
  | TicTacToeMessage message ->
    let newState, returnMessage = TicTacToe.update message state
    { state with ticTacToe = newState }, returnMessage
  | TestPageMessage message ->
    let newState, returnMessage = Test.update message state
    ({ state with test = newState }, returnMessage)
  | ExamplePageMessage message ->
    let newState, returnMessage = Example.update message state
    { state with example = newState }, returnMessage
  | DebugPageMessage message ->
    let newState, returnMessage = Debug.update message state
    { state with debug = newState }, returnMessage

/// shellUpdate: ShellMessage -> ShellState -> (ShellState * Cmd<ShellMessage>)
let shellUpdate message (state: ShellState) =
  // todo skicka ett cmd med nya debug
//  Core.Debug.debugShellMessage message
  (message, state)
  |> Core.Debug.debugMessage
  |> routeInput
/// shellView: ShellState -> (ShellMessage -> unit) -> IView<DockPanel>
/// är i praktiken just nu bara menyn
let shellView (shellState: ShellState) (dispatch: ShellMessage -> unit) =
  DockPanel.create [ DockPanel.children [ TabControl.create [ TabControl.tabStripPlacement Dock.Top
                                                              TabControl.viewItems [ TabItem.create [ TabItem.header
                                                                                                        "TicTacToe"
                                                                                                      TabItem.content (
                                                                                                        View.TicTacToe.view
                                                                                                          shellState
                                                                                                          (TicTacToeMessage
                                                                                                           >> dispatch)
                                                                                                      ) ]
                                                                                     TabItem.create [ TabItem.header
                                                                                                        "Example"
                                                                                                      TabItem.content (
                                                                                                        View.Example.view
                                                                                                          shellState
                                                                                                          (ExamplePageMessage
                                                                                                           >> dispatch)
                                                                                                      ) ]
                                                                                     TabItem.create [ TabItem.header
                                                                                                        "EmptyTab"
                                                                                                      TabItem.content (
                                                                                                        View.Test.view
                                                                                                          shellState
                                                                                                          (TestPageMessage
                                                                                                           >> dispatch)
                                                                                                      ) ]
                                                                                     TabItem.create [ TabItem.header
                                                                                                        "Debug"
                                                                                                      TabItem.content (
                                                                                                        View.Debug.view
                                                                                                          shellState
                                                                                                          (DebugPageMessage
                                                                                                           >> dispatch)
                                                                                                      ) ] ] ] ] ]
