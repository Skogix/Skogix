module Main.Message

type CounterMessage =
  | IncrementIfRunning
  | Increment
  | IncrementDelayed 
  | Decrement
  | Reset
  | RunningTrue
  | RunningFalse
  | GetState
type EmptyTabMessage =
  | DoNothing
type ShellMessage =
  | CounterMessage of CounterMessage
  | EmptyTabMessage of EmptyTabMessage
