module Core.State

open Core.Domain.TicTacToe
/// State.fs
/// all output som går till UI
/// shellState låter moduler dela state
type ExampleOutput = { running: bool; count: int }

type TestOutput =
  {
    stringValue: string
    intValue: int
    debugOutput: string list
  }

type DebugOutput =
  {
    messages: string list
    enabled: bool
  }

type TicTacToeOutput =
  {
    squareMap: SquareMap
    currentTurn: Player
    winner: Player option
  }

type ShellState =
  {
    test: TestOutput
    example: ExampleOutput
    debug: DebugOutput
    ticTacToe: TicTacToeOutput
  }
