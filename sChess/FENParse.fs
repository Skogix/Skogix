module sChess.FENParse
open sChess.Entities
open sChess.FEN
open sParse
open sParse.Common
open sParse.SParser

type FENValue =
    | Board of Board
    | ActiveColor of ActiveColor
    | CastlingRights of CastlingRight
    | EnPassant of EnPassant
    | HalfMove of  HalfMove
    | FullMove of FullMove
    | Piece of Piece
let pRank :Parser<Glyph>= anyOf (FEN.rankGlyphs)
let pRow :Parser<Glyph>= anyOf (['1'..'8'])
let pFenEmptyFiller = pRow
let pColumn :Parser<Glyph>= anyOf (['a'..'h'] @ ['A'..'H'])
let pMove = 
  pRank 
  .>>. (pColumn 
  .>>. pRow)
// let pChessMove :Parser<Move>= 
//   let resultToMove result = 
//     let pieceGlyph, (Column:Glyph, Row:Glyph)= result
//     let piece = glyphToPiece pieceGlyph
//     let x: X = glyphToX Column
//     let y: Y = glyphToY Row
//     let position = {x=x;y=y}
//     (piece, position)
//   pMove
//   |> mapParse resultToMove
  // let array3 = [| for i in 1 .. 10 -> i * i |]
let createStringWithDots (n:char) =
  let int = System.Int32.Parse(n |> string)
  let array = [for i in 1..int -> '.'] 
  System.String.Concat(Array.ofList(array))
let replaceNumberWithDots (s:string) =
  s 
  |> Seq.toList
  |> List.map(fun x -> 
    match x |> System.Char.IsDigit with
    | true -> createStringWithDots x
    | false -> x.ToString())

let pFEN =
  let slash = parseChar '/'
  let rank = pRank
  let empty = pFenEmptyFiller
  let a = 
    manyChars (rank <|> empty) .>> (slash <|> parseWhitespace)
  let list = many a
  list
  |> mapParse(fun x -> 
    x 
    |> List.map(fun x -> replaceNumberWithDots x )
    |> List.concat)
let getFENBoardString str =
  let boardStr =
    match run pFEN str with
    | ParseSuccess (x,y) -> x 
    | ParseFailure (x,y) -> []
    |> System.String.Concat
  boardStr
let mapPieceListToPositions (pieces:string) =
  let rec fn (pieces:Piece option list) (output:(Piece * Position) list) (pos:Position) =
    match pieces with
    | head::tail -> 
      match head with
      | x ->
        match x with
        | Some x -> []
        | None -> []
    | [] -> output
  let list =
    pieces
    |> List.ofSeq
    |> List.map(fun x -> x |> glyphToPiece)
  positions
  |> List.zip list
let getFENBoard str :Board = 
  let squares =
    str
    |> getFENBoardString
    |> mapPieceListToPositions
    |> List.map(fun (piece,position) -> Square.create position piece)
  {squares=squares}