module Main.EmptyTab

open System
open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Avalonia.Threading
open Elmish
open Main.Message

type State = {state: string}
  
let update message state =
  match message with
  | DoNothing ->
    printfn "DONOTHING GOGO!"
    state, Cmd.ofMsg (ShellMessage.EmptyTabMessage DoNothing)
  
  
let init = {state = "Huhu"}, Cmd.none
let view state dispatch =
  StackPanel.create [
    StackPanel.children [
      TextBlock.create [
        TextBlock.text "Huhu"
      ]
      Button.create [
        Button.dock Dock.Bottom
        Button.onClick (fun _ -> dispatch (DoNothing))
        Button.content "+" ]
      ]
    ]