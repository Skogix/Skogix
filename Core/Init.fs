module Core.Init

open Core.Domain.TicTacToe
open Core.Input
open Elmish
open State
/// ToDo
/// hämta settings via Cmd senare

/// Init.fs
/// all initial state
let exampleInit = { running = false; count = 0 }, Cmd.none

let testInit =
  {
    stringValue = "Huhu"
    intValue = 0
  },
  Cmd.none

let debugInit () =
  let debugState = { enabled = true; messages = [] }
  debugState, Cmd.none

let ticTacToeInit () =
  let squareMap =
    [
      for i in [ 0 .. 8 ] do
        (i, None)
    ]
    |> Map.ofList

  let state =
    {
      squareMap = squareMap
      winner = None
      currentTurn = Domain.TicTacToe.X
    }

  state, Cmd.none
/// shellInit: ShellState * Cmd<ShellMessage>
let shellInit: ShellState * Cmd<ShellMessage> =
  // om init kor ett command så kommer det här inte att funka, t.ex hämta data
  let exampleInit, exampleCmd = exampleInit
  let testInit, testCmd = testInit
  let debugInit, debugCmd = debugInit ()
  let ticTacToeInit, ticTacToeCmd = ticTacToeInit ()

  {
    State.example = exampleInit
    State.test = testInit
    State.debug = debugInit
    State.ticTacToe = ticTacToeInit
  },
  Cmd.batch [ exampleCmd ]
