import { Pipe, PipeTransform } from '@angular/core';
import {Item} from '../home/home.component';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform( news: Item[], page: number = 0, search: string = '' ): Item[] {
    //debugger;
    if ( search.length === 0 )
      return news.slice(page, page + 10);
    
    const filteredPokemons = news.filter( item => item.title.includes( search ) );
    return filteredPokemons.slice(page, page + 10);

  }

}