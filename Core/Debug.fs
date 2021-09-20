module Core.Debug.State

open System
open System.Collections.Generic

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
        return! loop []
    }
    loop []
    )
  member this.Send message = mailbox.Post (Send message)
  member this.Get() = mailbox.PostAndAsyncReply (Get)