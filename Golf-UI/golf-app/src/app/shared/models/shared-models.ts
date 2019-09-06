export interface Player {
  id: number;
  playerName: string;
  team_id: number;
  totalScore: number;
}

export interface AddScore {
  hole_nr: number;
  score: number;
  strokes: number;
  playerId: number;
}

export interface ScoreCard {
  naam: string;
  points: number;
  strokes: number;
  holesLeft: number;
}

export interface PlayerScoreCard {
  hole_nr: number;
  score: number;
  strokes: number;
}