import { Component } from '@angular/core';
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'XYZEngineeringProject.UI';
  constructor(private translate: TranslateService) {
    //TODO ustawić tłumaczenie na wybrane przez użytkownika
    translate.setDefaultLang('pl');
    translate.use('pl');
  }
}
