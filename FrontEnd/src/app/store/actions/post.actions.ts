import { createAction, props } from '@ngrx/store';
import { Post } from '../../models/post.model';

export const listPosts = createAction('[Post] Load Posts');
export const listPostsSuccess = createAction('[Post] Load Posts Success', props<{ posts: Post[] }>());
export const listPostsFailure = createAction('[Post] Load Posts Failure', props<{ error: string }>());

export const createPost = createAction('[Post] Create Post', props<{ post: Post }>());
export const createPostSuccess = createAction('[Post] Create Post Success', props<{ post: Post }>());
export const createPostFailure = createAction('[Post] Create Post Failure', props<{ error: string }>());

export const deletePost = createAction('[Post] Delete Post', props<{ id: number }>());
export const deletePostSuccess = createAction('[Post] Delete Post Success', props<{ id: number }>());
export const deletePostFailure = createAction('[Post] Delete Post Failure', props<{ error: string }>());