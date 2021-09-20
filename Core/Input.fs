module Core.Input
type TestInput =
  | DoNothing
type ExampleInput =
  | IncrementIfRunning
  | Increment
  | IncrementDelayed 
  | Decrement
  | ResetCount
  | RunningTrue
  | RunningFalse
type DebugInput =
  | Show
type ShellMessage =
  | TestPageMessage of TestInput
  | ExamplePageMessage of ExampleInput
  | DebugPageMessage of DebugInput
