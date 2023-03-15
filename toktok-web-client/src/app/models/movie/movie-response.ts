import { Reaction } from "../reaction/reaction";

export interface MovieResponse {
  movieId: string;
  title: string;
  imageUrl: string;
  reactions: Reaction[];
}
