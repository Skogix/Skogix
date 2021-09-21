module Main.Update.TicTacToeUpdate

open Core.State
open Elmish

let switchTurn (state:TicTacToeOutput) =
  match state.currentTurn with
  | Core.Domain.TicTacToe.Circle -> {state with currentTurn = Core.Domain.TicTacToe.Cross}
  | Core.Domain.TicTacToe.Cross -> {state with currentTurn = Core.Domain.TicTacToe.Circle}
let addMove move state =
  {state with squareMap = state.squareMap.Add(move, Some state.currentTurn)}
let tryAddMove move state =
  match state.squareMap.TryFind(move) with
  | Some square ->
    match square.IsSome with
    | true -> // squaren har redan en player
      Core.Result.Nay state
    | false -> Core.Result.Yay (addMove move state)
  | None -> // ska aldrig hända, får ett id som inte finns
    Core.Result.Nay state
let update (message:Core.Input.TicTacToeInput) (shellState:ShellState) =
  let state = shellState.ticTacToe
  let newState =
    match message with
    | Core.Input.TryPlaceMove move ->
//      state
//      |> addMove move
//      |> switchTurn
      state
      |> tryAddMove move
      |> Core.Result.SkogixOptionBind switchTurn
  let output = 
    match newState with
    | Core.Result.Yay yayState -> yayState
    | Core.Result.Nay nayState -> state
  output, Cmd.none
  
  

