import { Injectable } from '@angular/core';
import { UserDetail } from './user-detail.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { faCropSimple } from '@fortawesome/free-solid-svg-icons';
@Injectable({
  providedIn: 'root',
})
export class UserDetailService {
  constructor(private http: HttpClient) {}

  readonly baseURL = 'https://localhost:7075/User';

  formData: UserDetail = new UserDetail();
  list: UserDetail[] = [];

  postUserDetail() {
    this.formData.birthDate =
      this.formData.birthDate.substring(4, 8) +
      '-' +
      this.formData.birthDate.substring(2, 4) +
      '-' +
      this.formData.birthDate.substring(0, 2);

    return this.http.post(this.baseURL, this.formData);
  }

  getUserDetail(id: number) {
    let birthDate = new Date();
    this.formData = new UserDetail();
    lastValueFrom(this.http.get(this.baseURL + '/' + id)).then((res) => {
      if (res) {
        const ud = res as UserDetail;
        const teste = res as any
        if (ud.birthDate) {
          birthDate = new Date(ud.birthDate);

          this.formData = {
            id: teste.id,
            name: ud.name,
            lastName: ud.lastName,
            email: ud.email,
            schoolingId: ud.schoolingId,
            birthDate: birthDate.toLocaleString('pt-BR', {
              year: 'numeric',
              month: '2-digit',
              day: '2-digit',
            }) as string,
            schoolingDescription: '',
          } as UserDetail;
        }
      }
    });
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
          name: d.name as string,
          email: d.email as string,
          birthDate: birthDate.toLocaleString('pt-BR', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
          }) as string,
          id: d.id as number,
          lastName: d.lastName as string,
          schoolingId: d.schoolingId as number,
          schoolingDescription: d.schooling.description as string,
        });
      }
    });
  }
}
