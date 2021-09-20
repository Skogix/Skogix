module Main.View.ExampleView

open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open Core.Input
open Core.State

let view (shellState: Core.State.ShellState) (dispatch: ExampleInput -> unit) =
  let state = shellState.Example
  DockPanel.create [ DockPanel.children [ Button.create [ Button.dock Dock.Bottom
                                                          Button.height 50.
                                                          Button.onClick (fun _ -> dispatch (IncrementDelayed))
                                                          Button.content "incrementDelayed" ]
                                          Button.create [ Button.isVisible (not state.running)
                                                          Button.dock Dock.Bottom
                                                          Button.height 50.
                                                          Button.onClick (fun _ -> dispatch (RunningTrue))
                                                          Button.content "runningTrue" ]
                                          Button.create [ Button.isVisible state.running
                                                          Button.dock Dock.Bottom
                                                          Button.height 50.
                                                          Button.onClick (fun _ -> dispatch (RunningFalse))
                                                          Button.content "runningFalse" ]
                                          Button.create [ Button.height 50.
                                                          Button.dock Dock.Bottom
                                                          Button.onClick (fun _ -> dispatch (ResetCount))
                                                          Button.content "reset" ]
                                          Button.create [ Button.height 50.
                                                          Button.dock Dock.Bottom
                                                          Button.onClick (fun _ -> dispatch (Decrement))
                                                          Button.content "-" ]
                                          Button.create [ Button.height 50.
                                                          Button.dock Dock.Bottom
                                                          Button.onClick (fun _ -> dispatch (Increment))
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
