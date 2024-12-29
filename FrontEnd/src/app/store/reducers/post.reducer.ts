import { createReducer, on } from '@ngrx/store';
import { Post } from '../../models/post.model';
import * as PostActions  from '../../store/actions/post.actions';

export interface PostState {
  posts: Post[];
  error: any;
}

export const initialState: PostState = {
  posts: [],
  error: null
};

export const postReducer = createReducer(
  initialState,
  on(PostActions.listPostsSuccess, (state, { posts }) => ({ ...state, posts })),
  on(PostActions.createPostSuccess, (state, { post }) => ({ ...state, posts: [...state.posts, post] })),
  on(PostActions.deletePostSuccess, (state, { id }) => ({ ...state, posts: state.posts.filter(post => post.id !== id) }))
);