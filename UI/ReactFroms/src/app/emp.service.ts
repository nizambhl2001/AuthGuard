import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class EmpService {
  
private apiUrl = 'http://localhost:3000';
  constructor(private http:HttpClient) { 
  }

getReligions(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/religions`);
}
getDistricts(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/districts`);
}
 getdata():Observable<any[]>{
  return this.http.get<any[]>(`${this.apiUrl}/items`);
 }
  
 addItem(item: any): Observable<any> {
  return this.http.post<any>(`${this.apiUrl}/items`,item);
}

updateItem(item: any,id:number): Observable<any> {
  return this.http.put<any>(`${this.apiUrl}/items/${id}`, item);
}

DeleteItem(id: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/items/${id}`);
}
 
}
