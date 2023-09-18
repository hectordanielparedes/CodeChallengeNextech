import { Pipe, PipeTransform } from '@angular/core';
import {Item} from '../home/home.component';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform( news: Item[], page: number = 0, itemsPerPage: number = 10, search: string = '' ): Item[] {
    if ( search.length === 0 )
      return news.slice(page, page + itemsPerPage);
    
    news = news.filter( item => item.title.toLocaleLowerCase().includes( search.toLocaleLowerCase() ) );
    return news.slice(page, page + itemsPerPage);

  }

}