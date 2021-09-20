module Main.View.TestView

open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Core.Debug.State

let view state dispatch =
  StackPanel.create [
    StackPanel.children [
      TextBlock.create [
        TextBlock.text "Huhu"
      ]
    ]
  ]
