module Main.Update.TicTacToeUpdate

open Core.State
open Elmish
open Core.Result
open Core.Domain.TicTacToe

let switchTurn (state:TicTacToeOutput) =
  match state.currentTurn with
  | Core.Domain.TicTacToe.X -> {state with currentTurn = Core.Domain.TicTacToe.O}
  | Core.Domain.TicTacToe.O -> {state with currentTurn = Core.Domain.TicTacToe.X}
let checkWinner (state:TicTacToeOutput) =
  let winCombinations = [
    [0;1;2]
    [3;4;5]
    [6;7;8]
    
    [0;3;6]
    [1;4;7]
    [2;5;8]
    
    [0;4;8]
    [2;4;6]
  ]
  let tryGetWinner (winCombinations:int list)  =
    let squaresWithPlayer = winCombinations |> List.map(fun i -> state.squareMap.[i]) |> List.filter (fun x -> x.IsSome)
    let amountCross = squaresWithPlayer |> List.filter(fun x -> x.Value = Core.Domain.TicTacToe.O) |> List.length
    let amountCircle = squaresWithPlayer |> List.filter(fun x -> x.Value = Core.Domain.TicTacToe.X) |> List.length
    match amountCross = 3, amountCircle = 3 with
    | true, _ -> (Some O)
    | _, true -> (Some X)
    | _, _ -> None
  let rec recTryGetWinner posLists =
    match posLists with
    | [] -> None
    | head::tail ->
      match tryGetWinner head with
      | Some winner -> Some winner
      | None -> recTryGetWinner tail
  let winner =
    winCombinations
    |> recTryGetWinner
  match winner with
  | Some X -> {state with winner = Some X}
  | Some O -> {state with winner = Some O}
  | None -> state
let addMove move state =
  {state with squareMap = state.squareMap.Add(move, Some state.currentTurn)}
let tryAddMove move state =
//  printfn $"tryaddmove {move}: {state.squareMap}"
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
      |>> switchTurn
      |>> checkWinner
  let output = 
    match newState with
    | Core.Result.Yay yayState -> yayState
    | Core.Result.Nay nayState -> state
  output, Cmd.none
  
  

