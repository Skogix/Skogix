module Main.Update.TicTacToeUpdate

open Core.Input
open Core.State
open Elmish

let switchTurn (state) =
  match state.currentTurn with
  | Core.Domain.TicTacToe.Circle -> {state with currentTurn = Core.Domain.TicTacToe.Cross}
  | Core.Domain.TicTacToe.Cross -> {state with currentTurn = Core.Domain.TicTacToe.Circle}
let addMove move state =
  {state with squareMap = state.squareMap.Add(move, Some state.currentTurn)}

let update (message:TicTacToeInput) (shellState:ShellState) =
  let state = shellState.ticTacToe
  let newState = 
    match message with
    | TryPlaceMove move -> state |> addMove move |> switchTurn
  newState, Cmd.none
  
  

