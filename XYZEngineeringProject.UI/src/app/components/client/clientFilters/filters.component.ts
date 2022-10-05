import { Client } from '../../../models/client.model';
import { AfterViewChecked, ChangeDetectorRef, Component, EventEmitter, OnChanges, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Route, Router, ActivatedRoute } from '@angular/router';
import { ClientService } from 'src/app/services/client/client.service';
import { forkJoin, first, map } from 'rxjs';

@Component({
  selector: 'client-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css']
})
export class ClientFiltersComponent {

  @Output() filterChange = new EventEmitter();

  public clientListFilterForm: FormGroup;
  public clients: Client[] = [];
  public filledFilters: HTMLElement[] = [];

  private defaultFormValues = {
    firstName: '',
    //...
  }

  constructor(
    private readonly service: ClientService,
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly chr: ChangeDetectorRef,
    //...
  ) { 
    let filters;
    const queryParams = this.route.snapshot.queryParams;

    if (Object.entries(queryParams).length > 0) {
      filters = queryParams;
    }

    if (filters) {
      this.router.navigate(['.'], {
        relativeTo: this.route,
        queryParams: { ...filters}
      });
    }
   
    this.clientListFilterForm = new FormGroup({
      //tu nie jestem pewna składni, ale coś koło tego
      firstName: new FormControl(filters?.['firstName'] || this.defaultFormValues.firstName)
    },
    { validators: [/*...*/] });

    this.clientListFilterForm.valueChanges.subscribe(value => {
      if(this.clientListFilterForm.valid) {
        let isInInitialState = true;

        for (const key in value) {
          if (value.hasOwnProperty(key)) {
           //tu coś będzie dalej
          }
        }

        if(!isInInitialState) {
          const params = {
            firstName: value.firstName || '',
          };

          const { firstName, ...rest} = this.route.snapshot.queryParams;

          this.router.navigate(['.'], {
            relativeTo: this.route, queryParams: {
              ...rest,
              ...params
            }
          }).then(() => {
            this.filterChange.emit(params);
          });
        }
        else {
          this.router.navigate(['.'], {
            relativeTo: this.route, queryParams: {}
          }).then(() => {
            this.filterChange.emit({});
          });            
        }
        
        this.getFilledFilters();
      }
    })

    // forkJoin([
    //   this.service.getAllClients(),
    //   //...
    // ]).pipe(map(([ clients ]) => {
    //   return {
    //     clients, /*...*/
    //   }; })).pipe(first()).subscribe({
    //     next: (res: {
    //       clients: Client[], /*...*/
    //     }) => {
    //       this.clients = res.clients.data;
    //       //...
    //     }
    //   });

  }

  private getFilledFilters() {
    const formValue = this.clientListFilterForm.value;

    this.filledFilters = [];

    for (const key in formValue) {
      if (formValue.hasOwnProperty(key)) {
        //do ogarnięcia
        // if(!this.filtersService.equalsDefaultValues(key, formValue[key], this.defaultFormValues)) {
        //   const filterElement = document.getElementById(key);
        //   if(filterElement) {
        //     this.filledFilters.push(filterElement);
        //   }
        // }
      }
    }
    this.chr.detectChanges();
  }
}
