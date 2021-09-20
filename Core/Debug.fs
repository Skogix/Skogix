module Core.Debug

open System
open Core.Input
/// ToDo
/// filtrering
/// har meddelandet skickats till output
/// fixa terminal-output

/// Debug.fs
/// hanterar all intern debugging
/// borde vara threadsafe
type DebugMessage = string
type DebugData = {
  Message: DebugMessage
  Time: DateTime
}
type DebugMailboxMessage =
  | Send of string
  | Get of AsyncReplyChannel<DebugData list>
  
  
  
type DebugManager() =
  let mailbox = MailboxProcessor.Start(fun inbox ->
    let rec loop (currentData: DebugData list) = async {
      let! mail = inbox.Receive()
      match mail with
      | Send debugMessage ->
        let debugData = {
          Message = debugMessage
          Time = DateTime.Now }
        return! loop (debugData :: currentData)
      | Get (rc) ->
        rc.Reply currentData
        return! loop currentData
    }
    loop []
    )
  member this.Send message = mailbox.Post (Send message)
  member this.Get() = mailbox.PostAndAsyncReply (Get)
// just nu räcker det med en manager, får se senare om varje projekt skapar sin egen
let debugManager = DebugManager()
/// debug: string -> unit
/// skicka en enkel string till managern, används mest for direkt testing / debugging
let debug str = debugManager.Send str

/// debugShellMessage: ShellMessage -> unit
/// formatterar och skickar debug-meddelanden till managern
let debugShellMessage (msg:Core.Input.ShellMessage) =
  let send str = debugManager.Send str
  match msg with
  | ShellMessage.DebugPageMessage debugMessage ->
    match debugMessage with
    | Update -> ()
    | Add str -> send $"debug.Add {str}"
  | ShellMessage.ExamplePageMessage exampleMessage ->
    match exampleMessage with
    | IncrementIfRunning -> ()
    | Increment -> send $"example.Increment"
    | IncrementDelayed -> send $"example.IncrementDelayed"
    | Decrement -> send $"example.Decrement"
    | ResetCount -> send $"example.ResetCount"
    | RunningTrue -> send $"example.RunningTrue"
    | RunningFalse -> send $"example.RunningFalse"
