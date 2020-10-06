import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {AuthService} from "./_services/auth.service";
// @ts-ignore
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthorizationComponent } from './authorization/authorization.component';
import { RegistrationComponent } from './registration/registration.component';
import { ProfileComponent } from './profile/profile.component';
import {AuthGuardService as AuthGuard} from "./_services/auth-guard.service";
import {JwtModuleOptions, JwtHelperService, JwtModule, JWT_OPTIONS} from "@auth0/angular-jwt";


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AuthorizationComponent,
    RegistrationComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    // @ts-ignore
    JwtModule.forRoot(JWT_OPTIONS),
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'authorization', component: AuthorizationComponent},
      { path: 'registration', component: RegistrationComponent},
      { path: 'profile',
        component: ProfileComponent,
        canActivate: [AuthGuard]
      },
      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [
    AuthGuard,
    AuthService,
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
