import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Post } from '../../models/post.model';
import * as PostActions from '../../store/actions/post.actions';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css'],
})
export class PostListComponent implements OnInit {
  posts$: Observable<Post[]>;

  constructor(private store: Store<{ posts: Post[] }>) {
    this.posts$ = this.store.select(state => state.posts);
  }

  ngOnInit() {
    this.store.dispatch(PostActions.listPosts());
  }

  createPost() {
    const newPost: Post = { id: Math.random(), nombre: 'New Post', descripcion: 'Description' };
    this.store.dispatch(PostActions.createPost({ post: newPost }));
  }

  deletePost(id: number) {
    this.store.dispatch(PostActions.deletePost({ id }));
  }
}
