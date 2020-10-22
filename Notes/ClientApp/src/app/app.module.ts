import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {AuthService} from "./_services/auth.service";
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthorizationComponent } from './authorization/authorization.component';
import { RegistrationComponent } from './registration/registration.component';
import { ProfileComponent } from './profile/profile.component';
import {AuthGuardService as AuthGuard} from "./_services/auth-guard.service";
import {JwtModuleOptions, JwtHelperService, JwtModule, JWT_OPTIONS} from "@auth0/angular-jwt";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { DialogElementsEmailComponent } from './dialog-elements-email/dialog-elements-email.component';
import { DialogElementsPasswordComponent } from './dialog-elements-password/dialog-elements-password.component';
import { HomeAuthorizedComponent } from './home-authorized/home-authorized.component';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AuthorizationComponent,
    RegistrationComponent,
    ProfileComponent,
    DialogElementsEmailComponent,
    DialogElementsPasswordComponent,
    HomeAuthorizedComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:44323"]
      }
    }),
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'authorization', component: AuthorizationComponent},
      { path: 'registration', component: RegistrationComponent},
      { path: 'profile',
        component: ProfileComponent,
        canActivate: [AuthGuard]
      },
      { path: 'home',
        component: HomeAuthorizedComponent,
        canActivate: [AuthGuard]
      },
      { path: '**', redirectTo: '' }
    ]),
    BrowserAnimationsModule,
    MaterialModule
  ],
  entryComponents: [
    DialogElementsEmailComponent,
    DialogElementsPasswordComponent
  ],
  providers: [
    AuthGuard,
    AuthService,
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
