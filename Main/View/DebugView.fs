module Main.View.DebugView

open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Core.Debug.State
open Core.State

let view (state:ShellState) dispatch =
  StackPanel.create [
    StackPanel.children [
      TextBlock.create [
        TextBlock.text (state.DebugManager.Get() |> Async.RunSynchronously |> string)
      ]
      Button.create [
        Button.content "Add debug"
        Button.onClick (fun _ -> state.DebugManager.Send "Test")
      ]
    ]
  ]
