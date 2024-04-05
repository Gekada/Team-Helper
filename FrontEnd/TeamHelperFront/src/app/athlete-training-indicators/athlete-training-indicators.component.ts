import { Component, OnInit, OnDestroy, Injectable, Input } from '@angular/core';
import { ChartData, ChartOptions,ChartDataset } from 'chart.js';
import { Athlete, TrainingVM, IndicatorsDataLookupDTO, Client} from '../api/api';

@Component({
  selector: 'app-athlete-training-indicators',
  templateUrl: './athlete-training-indicators.component.html',
  styleUrls: ['./athlete-training-indicators.component.css']
})
export class AthleteTrainingIndicatorsComponent implements OnInit {
  @Input() athlete: Athlete;
  @Input() training: TrainingVM;

  dataset: ChartDataset<'line'> = {label: "help", data: [1, 100, 1000, 5000]}
  salesData: ChartData<'line'> = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May'],
    datasets: [
      { label: 'Mobiles', data: [1000, 1200, 1050, 2000, 500], tension: 0.5 },
      { label: 'Laptop', data: [200, 100, 400, 50, 90], tension: 0.5 },
      { label: 'AC', data: [500, 400, 350, 450, 650], tension: 0.5 },
    ],
  };
  chartOptions: ChartOptions
  constructor(public client: Client){}
  ngOnInit(): void {
    this.client.getIndicatorsForAthlete({trainingId: this.training.id, athleteId:this.athlete.id}).subscribe(res=>{
      var pressureList: number[] = [];
      var temperatureList: number[] = [];
      var pulseList: number[] = [];
      var indexlist: number[] = [0];
        res.indicators!.forEach(element => {
          pressureList.push(Number(element.bloodPressure));
          temperatureList.push(Number(element.temperature));
          pulseList.push(Number(element.pulse));
          indexlist.push(indexlist[indexlist.length-1]+1);
        });
        var temperatureLine: ChartDataset<'line'> = {label:"temperature",data:temperatureList}
        var pressureLine: ChartDataset<'line'> = {label:"pressure",data:pressureList}
        var pulseLine: ChartDataset<'line'> = {label:"pulse",data:pulseList}
        this.salesData = {
          labels: indexlist,
          datasets: [
            temperatureLine,
            pressureLine,
            pulseLine,
          ],
        };
        this.chartOptions = {
          responsive: true,
          plugins: {
            title: {
              display: true,
              text: this.athlete.name+"\'s "+'indicators data',
            },
          },
        };
    });
    if (this.training.isInprocess){
      setInterval(()=>{
        this.client.getIndicatorsForAthlete({trainingId: this.training.id, athleteId:this.athlete.id}).subscribe(res=>{
          var pressureList: number[] = [];
          var temperatureList: number[] = [];
          var pulseList: number[] = [];
          var indexlist: number[] = [0];
            res.indicators!.forEach(element => {
              pressureList.push(Number(element.bloodPressure));
              temperatureList.push(Number(element.temperature));
              pulseList.push(Number(element.pulse));
              indexlist.push(indexlist[indexlist.length-1]+1);
            });
            var temperatureLine: ChartDataset<'line'> = {label:"temperature",data:temperatureList}
            var pressureLine: ChartDataset<'line'> = {label:"pressure",data:pressureList}
            var pulseLine: ChartDataset<'line'> = {label:"pulse",data:pulseList}
            this.salesData = {
              labels: indexlist,
              datasets: [
                temperatureLine,
                pressureLine,
                pulseLine,
              ],
            };
        });
      }, 6000);
    }
  }
}