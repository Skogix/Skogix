module Core.State

open Core.Debug

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