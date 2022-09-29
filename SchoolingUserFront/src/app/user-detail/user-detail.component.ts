import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserDetail } from '../shared/user-detail.model';
import { UserDetailService } from '../shared/user-detail.service';
import { faPenAlt,faTrash } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.sass'],
})
export class UserDetailComponent implements OnInit {
  constructor(
    public service: UserDetailService,
    private changeDetector: ChangeDetectorRef,
    private router: Router
  ) {}

  faPenAlt = faPenAlt;
  faTrash = faTrash;

  ngOnInit(): void {
    this.service.refreshList();
    this.changeDetector.detectChanges();
    console.log(JSON.stringify(this.service.list));
  }
}
