module Core.Init
open Core.Debug.State
open Elmish
open State
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
    manager = Debug.State.debugManager
    messages = []}
  debugState, Cmd.none
