module Core.Init
open Core.Debug
open Elmish
open State
/// ToDo
/// hämta settings via Cmd senare

/// Init.fs
/// all initial state
let exampleInit =
  {
    running = false
    count = 0 }
  , Cmd.none
let testInit = {
  stringValue = "Huhu"
  intValue = 0
  debugOutput = []}, Cmd.none
let debugInit() =
  let debugState = {
    manager = Debug.debugManager
    messages = []}
  debugState, Cmd.none
