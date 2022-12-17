module sChess.Entities
type Color = Black | White
type Rank = Pawn | Rook | Knight | Bishop | Queen | King
type X = int
type Y = int
type Piece = {color:Color;rank:Rank}
type Position = {x:X;y:Y} with 
    static member create x y = {x=x;y=y}
type Move = Piece * Position
type SquareID = int
type Square = {position:Position;piece:Piece option} with 
    static member create pos piece = {position=pos;piece=piece}
type Board = {squares:Square list}
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
