import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from 'src/app/auth/auth-service.service';
import { TranslateService } from '@ngx-translate/core';
@Component({
  selector: 'app-coachn-nav-bar',
  templateUrl: './coachn-nav-bar.component.html',
  styleUrls: ['./coachn-nav-bar.component.css']
})
export class CoachnNavBarComponent implements OnInit {

  constructor(public authService: AuthServiceService, private translate: TranslateService) { 
    translate.addLangs(['en', 'ua']);
  }

  ngOnInit(): void {
  }
  
  switchLanguage(language: string) {
    this.translate.use(language); // Switch to the selected language
  }
}
