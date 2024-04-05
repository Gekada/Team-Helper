import { Component, OnInit } from '@angular/core';
import {MatCardModule} from '@angular/material/card'
@Component({
  selector: 'app-coach-card',
  templateUrl: './coach-card.component.html',
  styleUrls: ['./coach-card.component.css'],
  standalone: true,
  imports: [MatCardModule]
})
export class CoachCardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
