module Core.Test.State

open Elmish

type State = {
  stringValue: string
  intValue: int
}
let init = {stringValue = "Test"; intValue = 0}, Cmd.none
