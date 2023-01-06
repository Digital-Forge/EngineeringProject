import { TranslateService } from '@ngx-translate/core';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'dateCustom'})
export class DateCustom implements PipeTransform {

    constructor(
        private translateService: TranslateService
    ){}
  transform(date: Date, type: string): string {
    let datePiped = '';

    date = new Date(date);

    if (type == 'fullLong') {
      datePiped = this.translateService.instant('Weekday.'+ date.getDay().toString()) + ', ' + date.getDate() + ' ' + this.translateService.instant('Month.' + (date.getMonth() + 1).toString()) + ' ' + date.getFullYear().toString();
    }
    else if (type == 'fullShort') {
      datePiped = date.getDate().toString() + ' ' + this.translateService.instant('Month.' + (date.getMonth() + 1).toString()) + ' \'' + date.getFullYear().toString().substring(2);
    }
    else if (type == 'short') {
      datePiped = date.getDate().toString().padStart(2, "0") + '.' + (date.getMonth() + 1).toString().padStart(2, "0")+ '.' + date.getFullYear().toString();
    }
    else if (type == 'dayMonth') {
      datePiped = date.getDate().toString() + ' ' + this.translateService.instant('Month.' + (date.getMonth() + 1).toString());
    }
    else if (type == 'shortTime') {
      datePiped = date.getDate().toString() + ' ' + this.translateService.instant('Month.' + (date.getMonth() + 1).toString()).substring(0, 3) + ' ' + ((new Date().getFullYear() != date.getFullYear()) ? ( + date.getFullYear().toString()) : '') + ' ' + date.getHours().toString().padStart(2, "0") + ':' + date.getMinutes().toString().padStart(2, "0");
    }

    return datePiped;
  }
}