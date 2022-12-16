module sChess.Entities
type Color = Black | White
type Rank = Pawn | Rook | Knight | Bishop | Queen | King
type X = int
type Y = int
type Piece = {color:Color;rank:Rank}
type Position = {x:X;y:Y}
type Square = {position:Position;piece:Piece option}
type BoardID = int
type Board = {squares:Square list;id:BoardID}
type Column = Square list
type Row = Square list
type ActiveColor = Color
type Castling = Rank*Color
type CastlingRight = Castling list
type EnPassant = Position
type Clock = int
type HalfMove = Clock
type FullMove = Clock
type FEN = {
    board:Board
    activeColor:ActiveColor
    castlingRight:CastlingRight
    enPassant:EnPassant
    halfMove:HalfMove
    fullMove:FullMove
}
