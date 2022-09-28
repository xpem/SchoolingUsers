import { Injectable } from '@angular/core';
import { UserDetail } from './user-detail.model';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class UserDetailService {
  constructor(private http: HttpClient) {}

  readonly baseURL = 'https://localhost:7075/User';

  formData: UserDetail = new UserDetail();
  list: UserDetail[] = [];

  postUserDetail() {
    this.formData.BirthDate =
      this.formData.BirthDate.substring(4, 8) +
      '-' +
      this.formData.BirthDate.substring(2, 4) +
      '-' +
      this.formData.BirthDate.substring(0, 2);

    return this.http.post(this.baseURL, this.formData);
  }

  refreshList() {
    let birthDate = new Date();
    this.list = [];
    lastValueFrom(this.http.get(this.baseURL)).then((res) => {
      for (const d of res as any) {
        console.log(d.schooling.description);
        if (d.birthDate) {
          birthDate = new Date(d.birthDate);
        }

        this.list.push({
          Name: d.name as string,
          Email: d.email as string,
          BirthDate: birthDate.toLocaleString('pt-BR', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
          }) as string,
          Id: d.id as number,
          LastName: d.lastName as string,
          SchoolingId: d.schoolingId as number,
          SchoolingDescription: d.schooling.description as string,
        });
      }
    });
  }
}
