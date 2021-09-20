module Core.State

open Core.Debug
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
type ShellState = {
  Test: TestOutput
  Example: ExampleOutput
  Debug: DebugOutput
}