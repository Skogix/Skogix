module Core.Test.State

open Elmish

type State = {
  stringValue: string
  intValue: int
  debugOutput: Core.Debug.State.DebugData list
}
let init = { stringValue = "Test"; intValue = 0; debugOutput = [] }, Cmd.none
