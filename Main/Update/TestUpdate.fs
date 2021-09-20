module Main.TestPage

open Elmish
open Core.Input
open Core.Test
open Core.State

  
let update (message:TestInput) (state:ShellState)=
  let testState = state.Test
  match message with
  | DoNothing -> Update.doNothing testState, Cmd.none
  
  