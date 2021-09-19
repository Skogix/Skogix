module TestProgram


open System
open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.FuncUI.DSL
open Elmish


type State = {Count: int}
type Msg =
  | Increment of int
  | IncrementRandom
  | CmdIncrementRandom
let init() = {Count = 0}, Cmd.none
let private random = Random()
let private incrementRandom() = (Increment(random.Next(0,100)))
let update msg state =
  match msg with
  | Increment value -> {state with Count = state.Count + value}, Cmd.none
  | IncrementRandom -> state, Cmd.ofMsg CmdIncrementRandom
  | CmdIncrementRandom -> state, Cmd.ofMsg (incrementRandom())
let view (state: State) dispatch =
  StackPanel.create [
    StackPanel.children [
      TextBlock.create [
        TextBlock.text "Huhu"
      ]
    ]
  ]
let program = Program.mkProgram init update view
program