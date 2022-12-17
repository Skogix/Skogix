open System
open sChess.FENParse
open sChess.FEN
open sParse.Common
open sChess.Entities

Console.Clear()
let print x = printfn "%A" x

let board = 
  let result :Result<string list * string> = 
    run sChess.FENParse.pFEN sChess.FEN.game1
  match result with
  | ParseSuccess (result, msg) 
    -> result
  | ParseFailure (x,y) 
    -> []

getFENBoard game2
|> printBoard
Console.ReadLine() |> ignore
