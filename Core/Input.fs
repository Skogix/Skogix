module Core.Input
/// Input.fs
/// innehåller all kommunikation mellan UI -> Core
/// shellmessage är sättet moduler kan prata med varandra via UI
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
  | Update
  | Add of string
type ShellMessage =
  | TestPageMessage of TestInput
  | ExamplePageMessage of ExampleInput
  | DebugPageMessage of DebugInput
