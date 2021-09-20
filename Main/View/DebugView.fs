module Main.View.DebugView

open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Core.Debug.State
open Core.State

let view (state:ShellState) dispatch =
  StackPanel.create [
    StackPanel.children [
      let list = state.Debug.manager.Get() |> Async.RunSynchronously
      for str in list do
        TextBlock.create [
          TextBlock.text (str |> string)
        ]
      Button.create [
        Button.content "Add debug"
        Button.onClick (fun _ -> state.Debug.manager.Send "Test")
      ]
    ]
  ]
