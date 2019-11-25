import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { HttpClient } from '@angular/common/http';
import { Data } from 'src/app/shared/data';

@Component({
  selector: 'app-bar-chart-display',
  templateUrl: './bar-chart-display.component.html',
  styleUrls: ['./bar-chart-display.component.css']
})
export class BarChartDisplayComponent implements OnInit {
  chart = [];
  data: Data[];
  category = [];
  count = [];
  url = "http://localhost:59127/api/claim/chart";

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.httpClient.get(this.url).subscribe((res: Data[]) => {
      res.forEach(y => {
        this.category.push(y.category);
        this.count.push(y.count);
      });
      this.chart = new Chart('canvas', {
        type: 'bar',
        data: {
          labels: this.category,
          datasets: [
            {
              data: this.count,
              backgroundColor: ["#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850"],
              borderColor: 'red',
              fill: true
            }
          ]
        },
        options: {
          responsive: false,
          legend: {
            display: false
          },
          title: {
            display: true,
            text: 'Category wise Claims Raised'
          },
          scales: {
            xAxes: [{
              display: true
            }],
            yAxes: [{
              ticks: {
                beginAtZero: true
            },
              display: true
            }],
          }
        }
      });
    });
  }

}
