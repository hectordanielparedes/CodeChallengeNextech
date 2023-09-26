import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, timeout, throwError } from 'rxjs';

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
  public isLoading: boolean = true;
  public itemsPerPageOptions: number[] = [10, 20, 50];
  public itemsPerPage: number = this.itemsPerPageOptions[0];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Item[]>(baseUrl + 'hackernews').pipe(
      timeout(180000),
      catchError((error)=>{
        if (error.name === 'TimeoutError') {
          return throwError('Request timed out. Please try again later.');
        }
        return throwError('An error occurred while fetching data.');
      })
    ).subscribe(result => {
      this.newestStories = result;
      this.updateHasMore();
      this.isLoading = false;
    }, error => console.error(error));
  }

  nextPage() {
    this.page = this.page + this.itemsPerPage;
    this.updateHasMore();
  }

  prevPage() {
    if ( this.page > 0 )
      this.page = this.page - this.itemsPerPage;
      this.updateHasMore();
  }

  onSearchTitle( search: string ) {
    this.page = 0;
    this.search = search;
    this.updateHasMore();
  }

  updateHasMore() {
    const filteredItems = this.newestStories.filter( item => item && item.title && item.title && item.title.toLocaleLowerCase().includes( this.search.toLocaleLowerCase() ) );

    const remainingItems = filteredItems.length - (this.page + this.itemsPerPage);
    this.hasMore = remainingItems > 0;
  }
  getCurrentPage(): number {
    return Math.floor(this.page / this.itemsPerPage) + 1;
  }

  getTotalPages(): number {
    const filteredItems = this.newestStories.filter(item =>
      item && item.title && item.title.toLowerCase().includes(this.search.toLowerCase())
    );
    return Math.ceil(filteredItems.length / this.itemsPerPage);
  }
  changeItemsPerPage(newItemsPerPage: number = 0) {
    this.itemsPerPage = Number(newItemsPerPage);
    this.page = 0;
    this.updateHasMore();
  }

}

export interface Item {
  title: string;
  url: string;
}
