
export interface Scorecard {
    player: Player[];
  }

export interface Player {
    name: string;
    hole: Hole[];
  }

export interface Hole {
    nr: number;
    stroke: number;
    score: number;
  }
