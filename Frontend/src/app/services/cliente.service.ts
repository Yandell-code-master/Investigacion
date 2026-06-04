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
}