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

export interface PlayerStroke {
  playerId: number;
  hole_nr: number;
  strokes: number;
}
interface MenuItemBase {
  title: string;
  route: string;
  icon: string;
}

type MenuItemChild = MenuItemBase;

export interface MenuItem extends MenuItemBase {
  description?: string;
  children?: MenuItemChild[];
}

export interface CourseHole {
  hole_nr: number;
  score: number;
  strokes: number;
}

export interface CourseHoles {
  hole_nr: number;
  par: number;
  stroke: number;
}


export interface GameDetails {
  id: number;
  password: string;
  gameName: string;
}

export interface CourseListDetails {
  id: number;
  courseName: string;
}

export interface NewCourse {
  courseName: string;
  holes: Hole[];
}

export interface Hole {
  holeNumber: number;
  par: number;
  stroke: number;
}
