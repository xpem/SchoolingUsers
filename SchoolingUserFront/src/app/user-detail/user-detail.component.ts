import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserDetail } from '../shared/user-detail.model';
import { UserDetailService } from '../shared/user-detail.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.sass'],
})
export class UserDetailComponent implements OnInit {
  constructor(
    public service: UserDetailService,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.service.refreshList();
    this.changeDetector.detectChanges();
    console.log(JSON.stringify(this.service.list));
  }
}
