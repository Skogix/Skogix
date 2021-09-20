module Core.State

open Core.Debug.State

type ExampleOutput = {
  running: bool
  count: int
}
type TestOutput = {
  stringValue: string
  intValue: int
  debugOutput: Core.Debug.State.DebugData list
}
type DebugManager = {
  manager: Debug.State.DebugManager
  messages: Debug.State.DebugData list
}
type ShellState = {
  Test: TestOutput
  Example: ExampleOutput
  Debug: DebugManager
}