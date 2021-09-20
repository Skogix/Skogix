module Core.Init
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
  let debugManager = new DebugManager()
  debugManager, Cmd.none
