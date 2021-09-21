module Main.Update.TicTacToeUpdate

open Core.State
open Elmish
open Core.Input
open Core.Domain.TicTacToe
open Core.TicTacToe.Update
open Core.Skogix


let update (message:TicTacToeInput) (shellState:ShellState) =
  let state = shellState.ticTacToe
  let newState =
    match message with
    | TryPlaceMove move -> 
      state
      |> tryAddMove move
      |>> switchTurn
      |>> checkWinner
      |> Result.Apply
  newState, Cmd.none
  
  

