module Main.View.TestView

open Avalonia.FuncUI.DSL
open Avalonia.Controls

let view state dispatch =
  StackPanel.create [
    StackPanel.children [
      TextBlock.create [
        TextBlock.text (state |> string)
      ]
    ]
  ]
