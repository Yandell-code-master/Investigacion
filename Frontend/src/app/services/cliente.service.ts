import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente.model';

@Injectable({
    providedIn: 'root'
})
export class ClienteService {

    private readonly http = inject(HttpClient);

    private readonly apiUrl =
        'https://localhost:7274/api/Clientes';

    list(): Observable<Cliente[]> {
        return this.http.get<Cliente[]>(
            `${this.apiUrl}/List`
        );
    }

    create(cliente: Cliente) {

        return this.http.post(
            `${this.apiUrl}/Create`,
            cliente,
            {
                responseType: 'text'
            }
        );
    }

    search(id: number) {

        return this.http.get<Cliente>(
            `${this.apiUrl}/Search?id=${id}`
        );

    }

    update(cliente: Cliente) {

        return this.http.put(
            `${this.apiUrl}/Update`,
            cliente,
            {
                responseType: 'text'
            }
        );

    }

    delete(id: number) {

        return this.http.delete(
            `${this.apiUrl}/Delete?id=${id}`,
            {
                responseType: 'text'
            }
        );

    }
}