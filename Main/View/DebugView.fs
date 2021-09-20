module Main.View.DebugView

open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Core.Debug.State

let view (state:DebugManager) dispatch =
  StackPanel.create [
    StackPanel.children [
      TextBlock.create [
        TextBlock.text (state.Get() |> Async.RunSynchronously |> string)
      ]
      Button.create [
        Button.content "Add debug"
        Button.onClick (fun _ -> state.Send "Test")
      ]
    ]
  ]
