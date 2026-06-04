import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Factura } from '../models/factura.model';

@Injectable({
    providedIn: 'root'
})
export class FacturaService {

    private readonly http = inject(HttpClient);

   
    private readonly apiUrl = 'https://localhost:7274/api/Facturas';

    list(): Observable<Factura[]> {
        return this.http.get<Factura[]>(
            `${this.apiUrl}/List`
        );
    }

    create(factura: Factura): Observable<any> {
        return this.http.post(
            `${this.apiUrl}/Create`,
            factura,
            {
                responseType: 'text'
            }
        );
    }

    search(id: number): Observable<Factura> {
        return this.http.get<Factura>(
            `${this.apiUrl}/Search?id=${id}`
        );
    }

    update(factura: Factura): Observable<any> {
        return this.http.put(
            `${this.apiUrl}/Update`,
            factura,
            {
                responseType: 'text'
            }
        );
    }

    delete(id: number): Observable<any> {
        return this.http.delete(
            `${this.apiUrl}/Delete?id=${id}`,
            {
                responseType: 'text'
            }
        );
    }
}