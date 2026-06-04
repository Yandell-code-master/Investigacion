import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Producto } from '../models/producto.model';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'https://localhost:7274/api/Productos';

  list(): Observable<Producto[]> {
    return this.http.get<Producto[]>(
      `${this.apiUrl}/List`
    );
  }

  create(producto: Producto) {
    return this.http.post(
      `${this.apiUrl}/Create`,
      producto,
      { responseType: 'text' }
    );
  }

  search(id: string) {
    return this.http.get<Producto>(
      `${this.apiUrl}/Search?id=${id}`
    );
  }

  update(producto: Producto) {
    return this.http.put(
      `${this.apiUrl}/Update`,
      producto,
      { responseType: 'text' }
    );
  }

  delete(id: string) {
    return this.http.delete(
      `${this.apiUrl}/Delete?id=${id}`,
      { responseType: 'text' }
    );
  }
}