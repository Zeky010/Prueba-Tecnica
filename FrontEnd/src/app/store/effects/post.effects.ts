import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { PostService } from '../../services/post.service';
import * as PostActions from '../actions/post.actions';

@Injectable()
export class PostEffects {
  constructor(private actions$: Actions, private postService: PostService) {}

  loadPosts$ = createEffect(() =>
    this.actions$.pipe(
      ofType(PostActions.listPosts),
      mergeMap(() =>
        this.postService.getPosts().pipe(
          map(posts => PostActions.listPostsSuccess({ posts })),
          catchError(error => of(PostActions.listPostsFailure({ error })))
        )
      )
    )
  );

  createPost$ = createEffect(() =>
    this.actions$.pipe(
      ofType(PostActions.createPost),
      mergeMap(action =>
        this.postService.createPost(action.post).pipe(
          map(post => PostActions.createPostSuccess({ post })),
          catchError(error => of(PostActions.listPostsFailure({ error })))
        )
      )
    )
  );

  deletePost$ = createEffect(() =>
    this.actions$.pipe(
      ofType(PostActions.deletePost),
      mergeMap(action =>
        this.postService.deletePost(action.id).pipe(
          map(() => PostActions.deletePostSuccess({ id: action.id })),
          catchError(error => of(PostActions.listPostsFailure({ error })))
        )
      )
    )
  );
}
