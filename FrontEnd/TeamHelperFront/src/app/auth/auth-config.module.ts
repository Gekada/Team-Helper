import { NgModule } from '@angular/core';
import { AuthModule } from 'angular-auth-oidc-client';


@NgModule({
    imports: [AuthModule.forRoot({
        config: {
              authority: 'https://localhost:5001',
              redirectUrl: window.location.origin,
              postLogoutRedirectUri: window.location.origin,
              clientId: 'team-helper-web-app',
              scope: 'openid profile email TeamHelperWebApi',
              responseType: 'code',
              logLevel: 3
          }
      })],
    exports: [AuthModule],
})
export class AuthConfigModule {
    
}
