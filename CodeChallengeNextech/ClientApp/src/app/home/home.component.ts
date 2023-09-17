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
    }, error => console.error(error));
  }

  nextPage() {
    this.page += 10;
  }

  prevPage() {
    if ( this.page > 0 )
      this.page -= 10;
  }

  onSearchTitle( search: string ) {
    this.page = 0;
    this.search = search;
  }
}

export interface Item {
  title: string;
  url: string;
}
