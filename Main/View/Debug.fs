module Main.View.Debug

open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Core.Input
open Core.State

let view (state: ShellState) dispatch =
  StackPanel.create [ StackPanel.children [ Button.create [ Button.content "Start"
                                                            Button.onClick (fun _ -> DebugInput.Start |> dispatch) ]
                                            Button.create [ Button.content "Stop"
                                                            Button.onClick (fun _ -> DebugInput.Stop |> dispatch) ]
                                            Button.create [ Button.content "Add debug"
                                                            Button.onClick (fun _ -> DebugInput.Add "Test" |> dispatch) ]
                                            let list = state.debug.messages

                                            for str in list do
                                              TextBlock.create [ TextBlock.text (str |> string) ] ] ]
