import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';
import { UserDetail } from '../shared/user-detail.model';
import { UserDetailService } from '../shared/user-detail.service';
import { faPenAlt, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.sass'],
})
export class UserDetailComponent implements OnInit {
  constructor(
    public service: UserDetailService,
    private changeDetector: ChangeDetectorRef,
    private router: Router,
    private toastr: ToastrService
  ) {}

  faPenAlt = faPenAlt;
  faTrash = faTrash;
  isLoading = true;

  ngOnInit(): void {
    this.service.refreshList();
    this.changeDetector.detectChanges();
    console.log(JSON.stringify(this.service.list));
    this.isLoading = false;
  }

  onDelete(id: number) {

    if(confirm("Tem certeza que deseja deletar este usuário?")){
    lastValueFrom(this.service.DeleteUserDetail(id)).then(
      (res) => {
        this.service.refreshList();
        this.toastr.info('Usuário deletado!');
      },
      (err) => {
        console.log(err);
      }
    );
    }
  }
}
