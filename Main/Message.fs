module Main.Message

type TestPageMessage =
  | IncrementIfRunning
  | Increment
  | IncrementDelayed 
  | Decrement
  | Reset
  | RunningTrue
  | RunningFalse
  | GetState
type ExamplePageMessage =
  | DoNothing
type ShellMessage =
  | TestPageMessage of TestPageMessage
  | ExamplePageMessage of ExamplePageMessage
