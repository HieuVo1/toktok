import { ReactionType } from "./reaction-type";

export interface Reaction {
  movieId: string;
  userId: string;
  reactionType: ReactionType;
}
