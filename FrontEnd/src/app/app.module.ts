import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { postReducer } from './store/reducers/post.reducer';
import { PostListComponent } from './components/post-list/post-list.component';

@NgModule({
  declarations: [PostListComponent],
  imports: [BrowserModule, 
    HttpClientModule, 
    StoreModule.forRoot({ posts: postReducer })
],
  providers: [],
  bootstrap: [PostListComponent],
})
export class AppModule {}