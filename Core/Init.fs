module Core.Init
open Core.Debug
open Core.Domain.TicTacToe
open Core.Input
open Elmish
open State
/// ToDo
/// hämta settings via Cmd senare

/// Init.fs
/// all initial state
let exampleInit =
  {
    running = false
    count = 0 }
  , Cmd.none
let testInit = {
  stringValue = "Huhu"
  intValue = 0
  debugOutput = []}, Cmd.none
let debugInit() =
  let debugState = {
    enabled = true
    messages = []}
  debugState, Cmd.none
let ticTacToeInit() =
  let squareMap =
    [ for i in [0..8] do
        (i, None) ] |> Map.ofList
  let state = {
    squareMap = squareMap
    winner = None
    currentTurn = O }
  state, Cmd.none