import { TranslateService } from '@ngx-translate/core';
import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({name: 'dateShort'})
export class DateShort implements PipeTransform {

    pipe = new DatePipe('en-GB');

    constructor(
        private translateService: TranslateService
    ){}
  transform(date: Date): string {
    let dateShort = new Date(date).getDate().toString() + ' ' + this.translateService.instant('Month.' + (new Date(date).getUTCMonth() + 1).toString()) + ' `' + new Date(date).getFullYear().toString().substring(2);

    return dateShort;
  }
}