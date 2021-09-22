module Core.State
open Core.Domain
open Core.Domain.TicTacToe
/// State.fs
/// all output som går till UI
/// shellState låter moduler dela state


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
}
type DebugOutput = {
  messages: string list
  enabled: bool
}
type TicTacToeOutput = {
  squareMap: Map<Id, Player option>
  winner: Player option
  currentTurn: Player
}
type ShellState = {
  test: TestOutput
  example: ExampleOutput
  debug: DebugOutput
  ticTacToe: TicTacToeOutput
}