import { UtilitiesService } from 'src/app/cores/services/utilities.service';
import { ReactionType } from './../../../../models/reaction/reaction-type';
import { Reaction } from './../../../../models/reaction/reaction';
import { MovieService } from './../../services/movie.service';
import { MovieResponse } from './../../../../models/movie/movie-response';
import { Component, Input } from '@angular/core';
import { finalize, tap } from 'rxjs';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent {
  @Input() movie!: MovieResponse;
  currentUserLiked!: boolean;
  likeCounts!: number;
  userId!: string;
  timeoutId: any;

  constructor(
    private readonly movieService: MovieService,
    private readonly utilitiesService: UtilitiesService
  ) {

  }

  Like(movieId: string) {
    if (this.currentUserLiked) {
      this.likeCounts--;
    }
    else {
      this.likeCounts++;
    }

    this.currentUserLiked = !this.currentUserLiked;

    this.sendRequestToServer(movieId);
  }

  ngOnInit(): void {
    this.userId = this.utilitiesService.getUserId();
    this.likeCounts = this.movie.reactions.length;
    if (this.movie.reactions.findIndex(r => r.userId == this.userId) != -1) {
      this.currentUserLiked = true;
    }
  }


  sendRequestToServer(movieId: string) {
    var reaction: Reaction = {
      movieId: movieId,
      userId: this.userId,
      reactionType: this.currentUserLiked ? ReactionType.Like : ReactionType.Dislike
    };

    this.movieService.reactMovie(reaction)
      .pipe(
        tap(result => {
          if (result.isSuccess) {
            console.log("reaction success");
          }
          else {
            console.log(result.errorMessage);
          }
        }),
      )
      .subscribe();
  }
}
