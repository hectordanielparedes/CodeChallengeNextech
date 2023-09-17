import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, timeout, throwError } from 'rxjs';
import {FilterPipe} from './filter.pipe'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public newestStories: Item[] = [];
  public page: number = 0;
  public search: string = '';
  public pageSize: number = 10;
  public hasMore: boolean = true;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Item[]>(baseUrl + 'hackernews').pipe(
      timeout(50000),
      catchError((error)=>{
        if (error.name === 'TimeoutError') {
          return throwError('Request timed out. Please try again later.');
        }
        return throwError('An error occurred while fetching data.');
      })
    ).subscribe(result => {
      console.log(result);
      this.newestStories = result;
      this.updateHasMore();
    }, error => console.error(error));
  }

  nextPage() {
    this.page += this.pageSize;
    this.updateHasMore();
  }

  prevPage() {
    if ( this.page > 0 )
      this.page -= this.pageSize;
      this.updateHasMore();
  }

  onSearchTitle( search: string ) {
    this.page = 0;
    this.search = search;
    this.updateHasMore();
  }

  updateHasMore() {    
    const filteredItems = this.newestStories.filter( item => item.title.includes( this.search ) );

    const remainingItems = filteredItems.length - (this.page + this.pageSize);
    this.hasMore = remainingItems > 0;
  }

}

export interface Item {
  title: string;
  url: string;
}
