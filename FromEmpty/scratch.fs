module FromEmpty.scratch

open System
open Avalonia.Controls
open Elmish
open Avalonia.FuncUI.DSL

type State = { Count: int }
type Message =
  | Increment of int
  | IncrementRandom
  | CommandIncrementRandom
let init() = {Count = 0}, Cmd.none
let private random = Random()
let private incrementRandom() =
  (Increment(random.Next(0,100)))
let update msg state =
  match msg with
  | Increment value -> {state with Count = state.Count + value}, Cmd.none
  | IncrementRandom -> state, Cmd.ofMsg CommandIncrementRandom
  | CommandIncrementRandom -> state, Cmd.ofMsg (incrementRandom())
let view state dispatch =
  StackPanel.create [
    StackPanel.children [
      TextBlock.create [
        TextBlock.text (sprintf "Count: %i" state.Count)
      ]
      Button.create [
        Button.onClick (fun _ -> dispatch (Increment 1))
        Button.content "+"
      ]
      Button.create [
        Button.onClick (fun _ -> dispatch IncrementRandom)
        Button.content "random"
      ]
    ]
  ]
