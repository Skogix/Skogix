open System
open sChess.FENParse
open sChess.FEN
open sParse.Common
open sChess.Entities

Console.Clear()
let print x = printfn "%A" x
// getFENBoard starting |> printBoard
// getFENBoard sChess.FEN.game1 |> printBoard
// getFENBoard sChess.FEN.game2 |> printBoard
// getFENBoard sChess.FEN.game3 |> printBoard
sChess.FENParse.replaceNumberWithDots game1
|> String.concat ""
|> fun x -> x.Split(" ") 
|> fun x -> x.[0]
|> print
// |> List.iter(fun x -> x |> String.concat |> printfn "%O")
Console.ReadLine() |> ignore
