import { HistoryRequest } from 'src/app/shared/models/history-request.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root',
})
export class ConversionService {
  private url = `https://localhost:44320/api`;

  constructor(private http: HttpClient) {}

  getCelsius(fahrenheitTemp: number): Observable<number> {
    return this.http.get<number>(`${this.url}/temperature/celsius/${fahrenheitTemp}`);
  }

  getFahrenheit(celsiusTemp: number): Observable<number> {
    return this.http.get<number>(`${this.url}/temperature/fahrenheit/${celsiusTemp}`);
  }

  getKilograms(pounds: number): Observable<number> {
    return this.http.get<number>(`${this.url}/mass/kilograms/${pounds}`);
  }

  getPounds(kilograms: number): Observable<number> {
    return this.http.get<number>(`${this.url}/mass/pounds/${kilograms}`);
  }

  getKilometers(miles: number): Observable<number> {
    return this.http.get<number>(`${this.url}/length/kilometers/${miles}`);
  }

  getMiles(kilometers: number): Observable<number> {
    return this.http.get<number>(`${this.url}/length/miles/${kilometers}`);
  }

  create(history: HistoryRequest): Observable<any> {
    return this.http.post<HistoryRequest>(`${this.url}/history`, JSON.stringify(history));
  }
}
