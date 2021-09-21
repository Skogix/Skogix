module Core.State

open Core.Debug
open System
open Core.Domain
open Core.Domain.TicTacToe
/// State.fs
/// all output som går till UI
/// shellState låter moduler dela state
type ExampleOutput = {
  running: bool
  count: int
}
type TestOutput = {
  stringValue: string
  intValue: int
  debugOutput: DebugData list
}
type DebugOutput = {
  manager: DebugManager
  messages: DebugData list
}
type TicTacToeOutput = {
  squareMap: SquareMap
  currentTurn: Domain.TicTacToe.Player
  winner: Player option
  }
type ShellState = {
  test: TestOutput
  example: ExampleOutput
  debug: DebugOutput
  ticTacToe: TicTacToeOutput
}