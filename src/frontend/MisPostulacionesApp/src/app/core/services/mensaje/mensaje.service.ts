import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { Mensaje } from '../../models/mensaje.model';

@Injectable({
  providedIn: 'root',
})
export class MensajeService {

  private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  enviarMensaje(data: Mensaje): Observable<any> {
    return this.http.post(`${this.baseUrl}/mensaje/enviar`, data);
  }
}
