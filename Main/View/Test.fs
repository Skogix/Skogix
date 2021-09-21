module Main.View.Test

open Avalonia.FuncUI.DSL
open Avalonia.Controls

let view state dispatch =
  StackPanel.create [ StackPanel.children [ TextBlock.create [ TextBlock.text "Huhu" ] ] ]
