module Core.Domain.TicTacToe

type Player = O | X
type Id = int
type SquareMap = Map<Id, Player option>