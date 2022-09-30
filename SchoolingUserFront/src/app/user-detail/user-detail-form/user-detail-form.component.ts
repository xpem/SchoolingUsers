import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserDetail } from 'src/app/shared/user-detail.model';
import { UserDetailService } from 'src/app/shared/user-detail.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-user-detail-form',
  templateUrl: './user-detail-form.component.html',
  styleUrls: ['./user-detail-form.component.sass'],
})
export class UserDetailFormComponent implements OnInit {
  id: string;
  constructor(
    public service: UserDetailService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    if (this.id && Number(this.id) > 0) {
      this.service.getUserDetail(Number(this.id));
    }
  }

  onSubmit(form: NgForm) {
    let creating = true;
console.log("id:" + this.id)
    if (this.id && Number(this.id) > 0) {
      creating = false;
    }

    this.service.PostOrPutUserDetail(creating).subscribe({
      complete: () => {
        this.resetForm(form);
        if (creating) {
          this.toastr.success('Adicionado com sucesso!');
        } else {
          this.toastr.info('Atualizado com sucesso!');
        }
        //this.router.navigate(['/teste',{queryParams: {id: 0}}])
        this.router.navigateByUrl('');
      },
      error: (e) => {
        if (e.detail) {
          this.toastr.warning(e.detail);
        } else {
          console.log(e);
        }
      },
    });
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new UserDetail();
  }
}
