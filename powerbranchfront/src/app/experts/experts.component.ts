import { Component, OnInit } from '@angular/core';
import { Expert } from 'src/models/expert';
import { ExpertService } from 'src/services/expert.service';

@Component({
  selector: 'app-experts',
  templateUrl: './experts.component.html',
  styleUrls: ['./experts.component.css']
})
export class ExpertsComponent implements OnInit {
  public experts: Expert[];
  constructor(
    private expertService: ExpertService
  ) { }

  ngOnInit(): void {
    this.expertService.getExperts().subscribe(
      (result) => {
        this.experts = result;
      }
    );
  }

}
