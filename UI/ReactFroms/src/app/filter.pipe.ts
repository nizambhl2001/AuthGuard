import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(items: any[], searchText: string): any[] {
    if (!items) return [];
    if (!searchText) return items;

    searchText = searchText.toLowerCase();

    return items.filter(item => {
      return (item.fname.toLowerCase() + ' ' + item.lname.toLowerCase()).includes(searchText) ||
             item.mobile.toLowerCase().includes(searchText) ||
             item.religion.toLowerCase().includes(searchText) ||
             item.district.toLowerCase().includes(searchText);
    });
  }
}
