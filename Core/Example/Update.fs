module Core.Example.Update
open Core.State

let incrementCounter state = {state with count = state.count + 1}
let decrementCounter state = {state with count = state.count - 1}
let runningTrue state = {state with running = true}
let runningFalse state = {state with running = false}
