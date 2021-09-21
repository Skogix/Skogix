module Core.Domain.TicTacToe

type Player = Cross | Circle
type Id = int
type SquareMap = Map<Id, Player option>