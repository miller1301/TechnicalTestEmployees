import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { IEmployee } from '../interfaces/employee.interface';

const uriSanbox = 'https://localhost:44340/api/Employee'

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  constructor( private httpClient: HttpClient ) { }

  getAllEmployees(): Observable<IEmployee[]> {
    return this.httpClient.get<IEmployee[]>(uriSanbox).pipe(
      map(employee => {
        employee.map(x => {
          x.foto = this.convertImgEmployee(x.foto)
          return x;
        })
        return employee
      })
    );
  }

  getEmployee(id: number): Observable<IEmployee> {
    return this.httpClient.get<IEmployee>(`${uriSanbox}/${id}`).pipe(
      map(x => {
        x.foto = this.convertImgEmployee(x.foto)
        return x
      })
    );
  }

  createEmployee(employeeDto: IEmployee): Observable<Object> {
    return this.httpClient.post(`${uriSanbox}`, employeeDto);
  }

  updateEmployee(id: number, employeeDto: IEmployee): Observable<Object> {
    return this.httpClient.put(`${uriSanbox}/${id}`, employeeDto);
  }

  deleteEmployee(id: number): Observable<Object> {
    return this.httpClient.delete(`${uriSanbox}/${id}`)
  }

  convertImgEmployee(imgEnconde: string) {
    const bytes = atob(imgEnconde).split('').map(char => char.charCodeAt(0));
    const blob = new Blob([new Uint8Array(bytes)], { type: 'image/jpeg' });
    return URL.createObjectURL(blob);
  }

  getDataBinaryImg( formData: FormData ) {
    return this.httpClient.post(`${uriSanbox}/upload`, formData);
  }

}
