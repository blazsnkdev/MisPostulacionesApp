import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class PostulacionService {
  private baseUrl = environment.apiUrl;
  constructor(private http:HttpClient){}

  procesarIA(texto: string) {
  return this.http.post<any>(
    `${this.baseUrl}/postulacion/ia/procesar`,
    JSON.stringify(texto), 
    {
      headers: {
        'Content-Type': 'application/json'
      }
    }
  );
}


  registrarPostulacion(data: any) {
    return this.http.post(
      `${this.baseUrl}/postulacion`,
      data
    );
  }
}
