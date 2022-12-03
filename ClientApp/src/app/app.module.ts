import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule, Routes } from "@angular/router";
import { CommonModule } from "@angular/common";
import { NoopAnimationsModule } from "@angular/platform-browser/animations";
import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { DataTablesModule } from "angular-datatables";
import { TextareaComponent } from "./shared/form/textarea/textarea.component";
import { TextComponent } from "./shared/form/text/text.component";
import { SelectComponent } from "./shared/form/select/select.component";
import { ButtonComponent } from "./shared/form/button/button.component";
import { AreaModule } from "./area/area.module";
import { NgSelectModule } from "@ng-select/ng-select";
import { NgxSliderModule } from "@angular-slider/ngx-slider";

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { BoardAdminComponent } from './board/admin/board-admin.component';
import { BoardModeratorComponent } from './board/moderator/board-moderator.component';
import { BoardUserComponent } from './board/user/board-user.component';
//import { MatExpansionModule } from '@angular/material/expansion';
//import { MatDatepickerModule } from '@angular/material/datepicker';
//import { MatNativeDateModule } from '@angular/material/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'user', component: BoardUserComponent },
  { path: 'mod', component: BoardModeratorComponent },
  { path: 'admin', component: BoardAdminComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },

];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    TextareaComponent,
    TextComponent,
    SelectComponent,
    ButtonComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    BoardAdminComponent,
    BoardModeratorComponent,
    BoardUserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    NgSelectModule,
    ReactiveFormsModule,
    CommonModule,
    AreaModule,
    NgxSliderModule,
    DataTablesModule,
    NgbModule,
    FormsModule,
    RouterModule.forRoot(routes),
    NoopAnimationsModule,
    //MatExpansionModule,
    //MatDatepickerModule,
    //MatNativeDateModule,
    BrowserAnimationsModule
  ],
  bootstrap: [AppComponent],
  exports: [RouterModule],
//  providers: [MatDatepickerModule],
})
export class AppModule { }
