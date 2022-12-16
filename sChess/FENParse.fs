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
let pRank = anyOf (['1'..'8'])
let pPosition = anyOf (['1'..'8'])
let pPiece = pRank .>>. pPosition
let chessPiece = pPiece