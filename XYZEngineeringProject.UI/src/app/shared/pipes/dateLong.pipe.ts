import { TranslateService } from '@ngx-translate/core';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'dateCustom'})
export class DateCustom implements PipeTransform {

    constructor(
        private translateService: TranslateService
    ){}
  transform(date: Date, type: string): string {
    let datePiped = '';

    if (type == 'fullLong') {
      datePiped = this.translateService.instant('Weekday.'+ new Date(date).getDay().toString()) + ', ' + date.getDate() + ' ' + this.translateService.instant('Month.' + (new Date(date).getMonth() + 1).toString()) + ' ' + new Date(date).getFullYear().toString();
    }
    else if (type == 'fullShort') {
      datePiped = new Date(date).getDate().toString() + ' ' + this.translateService.instant('Month.' + (new Date(date).getMonth() + 1).toString()) + ' \'' + new Date(date).getFullYear().toString().substring(2);
    }
    else if (type == 'short') {
      datePiped = new Date(date).getDate().toString().padStart(2, "0") + '.' + (new Date(date).getMonth() + 1).toString().padStart(2, "0")+ '.' + new Date(date).getFullYear().toString();
    }
    else if (type == 'dayMonth') {
      datePiped = new Date(date).getDate().toString() + ' ' + this.translateService.instant('Month.' + (new Date(date).getMonth() + 1).toString());
    }

    return datePiped;
  }
}