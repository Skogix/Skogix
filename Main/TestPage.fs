module Main.TestPage

open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Elmish
open Main.Message

type State = {
  stringValue: string
  intValue: int
}
  
let update message state =
  match message with
  | DoNothing -> ()
  
  
let init = {stringValue = "Test"; intValue = 0}, Cmd.none
let view state dispatch =
  StackPanel.create [
    StackPanel.children [
      TextBlock.create [
        TextBlock.text (state |> string)
      ]
    ]
  ]