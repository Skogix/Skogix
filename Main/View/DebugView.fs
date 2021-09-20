module Main.View.DebugView

open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Avalonia.Threading
open Core.Debug.State
open Core.Input
open Core.State

let view (state:ShellState) dispatch =
  StackPanel.create [
    StackPanel.children [
      Button.create [
        Button.content "Add debug"
        Button.onClick (fun _ -> DebugInput.Add "Test" |> dispatch)
      ]
      let list = state.Debug.messages
      for str in list do
        TextBlock.create [
          TextBlock.text (str.Message |> string)
        ]
    ]
  ]
