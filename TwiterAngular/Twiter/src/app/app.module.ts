import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LeftMenuComponent } from './main/shared/left-menu/left-menu.component';
import { TrendsComponent } from './main/layout/trends/trends.component';
import { HeaderComponent } from './main/layout/header/header.component';
import { HomeComponent } from './main/content/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    LeftMenuComponent,
    TrendsComponent,
    HeaderComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
